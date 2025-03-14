
using OrdersApp;
using OrdersApp.Enums;
using OrdersApp.Extensions;

Display.Welcome();
ConsoleKeyInfo keyinfo;
var runApplication = true;
do
{
    keyinfo = Console.ReadKey();
    
    var testKey = keyinfo.Key == ConsoleKey.Escape ? "ESC" : keyinfo.KeyChar.ToString();
    var command = CommandsExtensions.GetByDescriptionName(testKey);

    switch (command)
    {
        case Commands.NewOrder:
            Display.NewOrder();
            break;
        case Commands.SendToWarehouse:
            Display.OrderToWarehouse();
            break;
        case Commands.SendToShipping:
            Display.SendOrder();
            break;
        case Commands.DisplayOrders:
            Display.DisplayOrders();
            break;
        case Commands.Exit:
            runApplication = false;
            break;
        default:
            Display.Welcome();
            break;
    }
}
while (runApplication);

