using System.Numerics;
using System.Runtime.CompilerServices;
using Raylib_cs;

Raylib.InitWindow(1300, 1000, "Ghost");
Raylib.SetTargetFPS(60);

while (!Raylib.WindowShouldClose())
{ 
    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.BLACK);
    Raylib.DrawText("press SPACE to start", 450, 500, 35, Color.WHITE);






    Raylib.EndDrawing();  
    
 }

   
