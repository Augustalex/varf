using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Computer.Screens;
using Space;
using TMPro;
using UnityEngine;

public class MarketScreen : MonoBehaviour
{
    private ScreenController _screenController;
    private TMP_Text _text;

    public ScreenRoot goBack;

    private float _cooldownUntil;

    private string[] _parts;
    private int _selectorIndex = 0;

    private float InputCooldown = .15f;
    private ScreenRoot _screenRoot;

    public Player player;

    public enum MarketState
    {
        Buying,
        Selling,
        Menu
    }

    private MarketState _marketState = MarketState.Menu;
    private MarketSystem _marketSystem;

    private readonly string[] _menuOptions =
    {
        "BACK",
        "BUY",
        "SELL",
    };

    void Awake()
    {
        _screenRoot = GetComponent<ScreenRoot>();
        _screenController = GetComponentInParent<ScreenController>();
        _text = GetComponentInChildren<TMP_Text>();

        ResetScreen();
    }

    void Start()
    {
        _marketSystem = new MarketSystem(player);

        _marketSystem.ListItemForPurchase(new PlayerItem
        {
            Category = ItemCategory.Engine,
            ItemName = "Dusty Tractor Engine"
        });
        _marketSystem.ListItemForPurchase(new PlayerItem
        {
            Category = ItemCategory.Fuel,
            ItemName = "Half-full tank of fuel"
        });
        _marketSystem.ListItemForPurchase(new PlayerItem
        {
            Category = ItemCategory.Pipe,
            ItemName = "Fine pipe"
        });
        _marketSystem.ListItemForPurchase(new PlayerItem
        {
            Category = ItemCategory.SmallCabin,
            ItemName = "Tiny lightly buckled cabin"
        });
    }

    private void ResetScreen()
    {
        GenerateText();
    }

    private void GenerateText()
    {
        if (_marketState == MarketState.Menu)
        {
            var header =
                $"f89b:19a9:d93d:bb14::MARKET.COM / logged_in\nFunds: ${player.funds}\n \n";
            _text.text = header + string.Join("\n", GetMenuItems());
        }
        else if (_marketState == MarketState.Buying)
        {
            var header =
                $"f89b:19a9:d93d:bb14::MARKET.COM / logged_in / BUY\nFunds: ${player.funds}\n \n";
            _text.text = header + string.Join("\n", GetBuyItems());
        }
        else if (_marketState == MarketState.Selling)
        {
            var header =
                $"f89b:19a9:d93d:bb14::MARKET.COM / logged_in / SELL\nFunds: ${player.funds}\n \n";
            _text.text = header + string.Join("\n", GetSellItems());
        }
    }

    private IEnumerable<string> GetMenuItems()
    {
        return _menuOptions.Select((part, index) => $"{(_selectorIndex == index ? ">" : "")}{part}");
    }

    private IEnumerable<string> GetBuyItems()
    {
        return new[] {"BACK"}
            .Concat(_marketSystem.ListedForPurchase().Select(item => $"{item.Name} ${item.Price}"))
            .Select((part, index) => $"{(_selectorIndex == index ? ">" : "")}{part}");
    }

    private IEnumerable<string> GetSellItems()
    {
        return new[] {"BACK"}
            .Concat(_marketSystem.GetItemsToSell().Select(item => $"{item.Name} ${item.Price}"))
            .Select((part, index) => $"{(_selectorIndex == index ? ">" : "")}{part}");
    }

    private void OnEnable()
    {
        _screenController.Next += OnNext;
        _screenController.Previous += OnPrevious;
        _screenController.OK += OnOK;
    }

    private void OnDisable()
    {
        _screenController.Next -= OnNext;
        _screenController.Previous -= OnPrevious;
        _screenController.OK -= OnOK;
    }

    private void OnOK()
    {
        if (Time.time < _cooldownUntil) return;
        _cooldownUntil = Time.time + InputCooldown;

        if (_selectorIndex >= 0)
        {
            if (_marketState == MarketState.Menu)
            {
                var selectedOption = _menuOptions[_selectorIndex];
                if (selectedOption == "BUY")
                {
                    _marketState = MarketState.Buying;
                }
                else if (selectedOption == "SELL")
                {
                    _marketState = MarketState.Selling;
                }
                else if (selectedOption == "BACK")
                {
                    _screenRoot.ChangeScreen(goBack);
                }
                else
                {
                    _selectorIndex = 0;
                }
            }
            else if (_marketState == MarketState.Buying)
            {
                if (_selectorIndex == 0)
                {
                    _marketState = MarketState.Menu;
                }
                else
                {
                    var itemIndex = _selectorIndex - 1; // Because first options is "BACK"
                    var items = _marketSystem.ListedForPurchase();
                    if (itemIndex >= 0 && itemIndex < items.Length)
                    {
                        _marketSystem.Buy(items[itemIndex]);
                    }
                    else
                    {
                        _selectorIndex = 0;
                    }
                }
            }
            else if (_marketState == MarketState.Selling)
            {
                if (_selectorIndex == 0)
                {
                    _marketState = MarketState.Menu;
                }
                else
                {
                    var itemIndex = _selectorIndex - 1; // Because first options is "BACK"
                    var items = _marketSystem.GetItemsToSell();
                    if (itemIndex >= 0 && itemIndex < items.Length)
                    {
                        _marketSystem.Sell(items[itemIndex]);
                    }
                    else
                    {
                        _selectorIndex = 0;
                    }
                }
            }

            ResetScreen();
        }
    }

    private void OnPrevious()
    {
        IncrementSelector(-1);
    }

    private void OnNext()
    {
        IncrementSelector(1);
    }

    public void IncrementSelector(int increment)
    {
        if (Time.time < _cooldownUntil) return;
        _cooldownUntil = Time.time + InputCooldown;

        _selectorIndex += increment;
        ResetScreen();
    }
}