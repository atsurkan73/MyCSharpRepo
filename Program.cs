/* 

*/

using SnakeGame;
using System.Linq.Expressions;

var rate = TimeSpan.FromMilliseconds(300);
var game = new Game();


using (var token = new CancellationTokenSource())
{
    var check = CheckKeyPresses(token);
    do
    {
        game.OnTick();
        game.Render();
        await Task.Delay(rate);
    }
    while (!game.IsGameOver);
    token.Cancel();
    await check;
}

async Task CheckKeyPresses(CancellationTokenSource cts)
{
    while (!cts.Token.IsCancellationRequested)
        if (Console.KeyAvailable)
        { 
            var key = Console.ReadKey(true).Key;
            game.OnKeyPress(key);
            await Task.Delay(10);
        }

}

