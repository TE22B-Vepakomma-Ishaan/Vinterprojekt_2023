
using System.Numerics;
using System.Runtime.CompilerServices;
using Raylib_cs;

Raylib.InitWindow(1300, 1000, "Ghost");
Raylib.SetTargetFPS(60);


string screen = "menu";


Rectangle gameBorder = new(25, 25, 800, 800);

Vector2 border = new(gameBorder.x, gameBorder.y);
Rectangle character = new(900, 400, 40, 40);
Texture2D PlayerSprite = Raylib.LoadTexture("GreenBall.png");

Vector2 movement = new(0,0);
float speed = 3;



while (!Raylib.WindowShouldClose())
{ 
    
   Raylib.BeginDrawing();

   // Character---------------------------------------------------------------------------------------------------------------
   
           movement = Vector2.Zero;

        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            movement.X +=2;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            movement.X -=2;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            movement.Y -=2;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            movement.Y +=2;
        }

        if (movement.Length() > 0)
        {
            movement = Vector2.Normalize(movement) * speed;
        }

        character.x += movement.X;
        character.y += movement.Y;
   


 
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
    Raylib.DrawRectangleLinesEx(gameBorder, 5, Color.PURPLE);
    
    
    
    
    Raylib.DrawRectangleRec(character, Color.BLANK);
    Raylib.DrawTexture(PlayerSprite, (int)character.x, (int)character.y, Color.PURPLE);

      

    }

    







    Raylib.EndDrawing();  
    
 }

   
