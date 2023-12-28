
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using Raylib_cs;

Random generator = new();



Raylib.InitWindow(1300, 1000, "Ghost game thing");
Raylib.SetTargetFPS(60);


string screen = "menu";


Rectangle gameBorder = new(25, 25, 800, 800);
Rectangle ghostBorder = new(950, 30, 240, 240);


Vector2 border = new(gameBorder.x, gameBorder.y);
Rectangle character = new(400, 400, 40, 40);
Texture2D PlayerSprite = Raylib.LoadTexture("GreenBall.png");


Vector2 movement = new(0, 0);
float movementX = 1;
float movementY = 1;
float speed = 10;
float Enemyspeed = 4;

Rectangle enemy = new(550, 40, 60, 80);


const int ScreenWidth = 800;
const int ScreenHeight = 800;
const int GridSize = 17;


int[,] Level = {
    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    {1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1},
    {1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1},
    {1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1},
    {1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1},
    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    {1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1},
    {1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1},
    {1, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1},
    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    {1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1},
    {1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0, 1, 0, 1},
    {1, 0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1},
    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    {1, 0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1},
    {1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1},
    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
};



List<Rectangle> CellHitbox = new();


// Raylib--------------------------------------------------------------------------------------------------------------------------------

while (!Raylib.WindowShouldClose())
{

    Raylib.BeginDrawing();

    // Movement---------------------------------------------------------------------------------------------------------------

    movement = Vector2.Zero;

    // if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
    // {
    //     movement.X += 2;
    // }
    // if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
    // {
    //     movement.X -= 2;
    // }
    // if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
    // {
    //     movement.Y -= 2;
    // }
    // if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
    // {
    //     movement.Y += 2;
    // }


            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                // Check for collision with walls
                if (!LevelCollision(CellHitbox, movementX + speed, movementY))
                {
                    movementX += speed;
                }
            }
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                if (!LevelCollision(CellHitbox, movementX - speed, movementY))
                {
                    movementX -= speed;
                }
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
            {
                if (!LevelCollision(CellHitbox, movementX, movementY + speed))
                {
                    movementY += speed;
                }
            }
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
            {
                if (!LevelCollision(CellHitbox, movementX, movementY - speed, ))
                {
                    movementY -= speed;
                }
            }    

    if (movement.Length() > 0)
    {
        movement = Vector2.Normalize(movement) * speed;
    }

    character.x += movement.X;
    character.y += movement.Y;

    // ------------------Micke-----------------------
    character.x = Math.Clamp(character.x, 25, 780);
    character.y = Math.Clamp(character.y, 25, 780);
    // ------------------Micke----------------------- 


    if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
    {
        character.x++;
    }

    

    // game-----------------------------------------------------------------------------------------------------------------------------
    
    
    
    if (screen == "menu")
    {

        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText("press SPACE to start", 450, 500, 35, Color.WHITE);

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            screen = "game";
        }


    }

    if (screen == "game")
    {

        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawRectangleLinesEx(gameBorder, 5, Color.PURPLE);
        Raylib.DrawRectangleLinesEx(ghostBorder, 5, Color.PURPLE);



        Raylib.DrawRectangleRec(enemy, Color.WHITE);


        // Raylib.DrawRectangleRec(character, Color.BLANK);
        // Raylib.DrawTexture(PlayerSprite, (int)character.x, (int)character.y, Color.PURPLE);
        // Raylib.DrawText($"{character.x}{character.y}", 1000, 800, 20, Color.WHITE);

        GameScreen(PlayerSprite, character, ScreenWidth, ScreenHeight, GridSize, Level, CellHitbox);
        


    }









    Raylib.EndDrawing();

}




// Functions-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


static void GameScreen(Texture2D PlayerSprite, Rectangle character, int ScreenWidth, int ScreenHeight, int GridSize, int[,] Level, List<Rectangle> CellHitbox)
{
    for (int i = 0; i < Level.GetLength(0); i++)
    {
        for (int j = 0; j < Level.GetLength(1); j++)
        {
            if (Level[i, j] == 1)
            {
                float CellWidth = ScreenWidth / (float)GridSize;
                float CellHeight = ScreenHeight / (float)GridSize;
                float x = j * CellWidth + 25;
                float y = i * CellHeight + 25;

                Rectangle cell = new((int)x, (int)y, (int)CellWidth, (int)CellHeight);
                CellHitbox.Add(cell);


            }

        }
    }
    foreach (var cell in CellHitbox)
    {
        Raylib.DrawRectangleRec(cell, Color.PURPLE);
    }
   

}

// static bool LevelCollision(List<Rectangle> Cellhitbox)
// {
//         Rectangle character = new(400, 400, 40, 40);


//     foreach (var cell in CellHitbox) 
//     {
//         if (Raylib.CheckCollisionRecs(cell, character))
//         {
//             return true; 
//         }
//     }
// }
