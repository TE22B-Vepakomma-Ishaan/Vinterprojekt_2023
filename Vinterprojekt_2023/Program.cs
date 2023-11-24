using System.Numerics;
using System.Runtime.CompilerServices;
using Raylib_cs;

Raylib.InitWindow(1300, 1000, "Ghost");
Raylib.SetTargetFPS(60);


string screen = "menu";









while (!Raylib.WindowShouldClose())
{ 
    
   Raylib.BeginDrawing();





 
   if(screen == "menu")
    {

        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText("press SPACE to start", 450, 500, 35, Color.WHITE);

        if(Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
           screen = "game"; 
        }
    } 

    if (screen == "game"){
    
    Raylib.ClearBackground(Color.BLACK);
    Raylib.DrawRectangleLines(20, 20, 800, 800,Color.DARKPURPLE);

    }

    







    Raylib.EndDrawing();  
    
 }

   
