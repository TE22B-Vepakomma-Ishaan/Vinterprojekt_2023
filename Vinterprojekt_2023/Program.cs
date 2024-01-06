
using System.ComponentModel;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

Random random = new();



Raylib.InitWindow(1300, 1000, "Ghost game thing");
Raylib.SetTargetFPS(60);


string screen = "menu";





Texture2D PlayerSprite = Raylib.LoadTexture("RedEye2.png");
Texture2D Tile = Raylib.LoadTexture("purple_tile2.png");
Texture2D CoinSprite = Raylib.LoadTexture("coin.png");



Raylib_cs.Rectangle character = new(410, 410, 42, 42);
Raylib_cs.Rectangle door = new(407, 450, 38, 65);


Vector2 movement = new(0, 0);
float speed = 7;


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



int n = 24;

List<Raylib_cs.Rectangle> CellHitbox = new();

List<Raylib_cs.Rectangle> PositionsList = new();

List<Raylib_cs.Rectangle> RandomizedPositions = new();










// Raylib--------------------------------------------------------------------------------------------------------------------------------

while (!Raylib.WindowShouldClose())
{

    Raylib.BeginDrawing();

    // Movement---------------------------------------------------------------------------------------------------------------

    movement = Vector2.Zero;



    if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
    {

        if (!LevelCollision(CellHitbox, character.x + speed, character.y))
        {
            character.x += speed;
        }
    }
    else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
    {
        if (!LevelCollision(CellHitbox, character.x - speed, character.y))
        {
            character.x -= speed;
        }
    }

    if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
    {
        if (!LevelCollision(CellHitbox, character.x, character.y + speed))
        {
            character.y += speed;
        }
    }
    else if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
    {
        if (!LevelCollision(CellHitbox, character.x, character.y - speed))
        {
            character.y -= speed;
        }
    }

    if (movement.Length() > 0)
    {
        movement = Vector2.Normalize(movement) * speed;
    }

    character.x += movement.X;
    character.y += movement.Y;



    
   







    // game-----------------------------------------------------------------------------------------------------------------------------



    if (screen == "menu")
    {

        Raylib.ClearBackground(Raylib_cs.Color.BLACK);
        Raylib.DrawText("press SPACE to start", 450, 500, 35, Raylib_cs.Color.WHITE);

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            screen = "game";
        }


    }

    if (screen == "game")
    {

        Raylib.ClearBackground(Raylib_cs.Color.BLACK);


        // Raylib.DrawText($"{character.x}{character.y}", 1000, 800, 20, Raylib_cs.Color.WHITE);

        LevelLayout(PlayerSprite, Tile, character, ScreenWidth, ScreenHeight, GridSize, Level, CellHitbox);


       CoinRandomize(random, ScreenWidth, ScreenHeight, GridSize, Level, PositionsList, RandomizedPositions);
        
        foreach (var Position in RandomizedPositions)
        {

            Raylib_cs.Rectangle coin = new(Position.x + 12, Position.y + 12, 20, 20);
            Raylib.DrawRectangleRec(coin, Raylib_cs.Color.BLANK);
            Raylib.DrawTexture(CoinSprite, (int)Position.x + 12, (int)Position.y + 12, Raylib_cs.Color.GOLD);




            if (Raylib.CheckCollisionRecs(character, coin))
             {
                
             }




        }


        Raylib.DrawRectangleRec(door, Raylib_cs.Color.WHITE);
        
        Raylib.DrawRectangleRec(character, Raylib_cs.Color.BLANK);
        Raylib.DrawTexture(PlayerSprite, (int)character.x, (int)character.y, Raylib_cs.Color.PURPLE);
        
        
        Raylib.DrawText($"{character.x}  {character.y}", 1000, 800, 30, Raylib_cs.Color.WHITE);
        Raylib.DrawFPS(1000, 500);
    }


    






    Raylib.EndDrawing();

}




// Functions-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


static void LevelLayout(Texture2D PlayerSprite, Texture2D Tile, Raylib_cs.Rectangle character, int ScreenWidth, int ScreenHeight, int GridSize, int[,] Level, List<Raylib_cs.Rectangle> CellHitbox)
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

                Raylib_cs.Rectangle cell = new((int)x, (int)y, (int)CellWidth, (int)CellHeight);

                CellHitbox.Add(cell);


            }
        }
    }
    // make better solution if time
    foreach (var cell in CellHitbox.Take(162))
    {
        Raylib.DrawRectangleRec(cell, Raylib_cs.Color.BLANK);
        Raylib.DrawTexture(Tile, (int)cell.x, (int)cell.y, Raylib_cs.Color.DARKPURPLE);
    }


}



static bool LevelCollision(List<Raylib_cs.Rectangle> CellHitbox, float x, float y)
{
    Raylib_cs.Rectangle character = new Raylib_cs.Rectangle(x, y, 35, 35);

    foreach (var cell in CellHitbox)
    {
        if (Raylib.CheckCollisionRecs(character, cell))
        {
            return true;
        }
    }

    return false;
}



static void CoinRandomize(Random random, int ScreenWidth, int ScreenHeight, int GridSize, int[,] Level, List<Raylib_cs.Rectangle> PositionsList, List<Raylib_cs.Rectangle> RandomizedPositions)
{
    for (int i = 0; i < Level.GetLength(0); i++)
    {
        for (int j = 0; j < Level.GetLength(1); j++)
        {
            if (Level[i, j] == 0 && (i != 9 || j != 9) && (i != 10 || j != 9) && (i != 10 || j != 9))     
            // 9 10, 9 11, 9 9
            {
                float CellWidth = ScreenWidth / (float)GridSize;
                float CellHeight = ScreenHeight / (float)GridSize;
                float x = j * CellWidth + 25;
                float y = i * CellHeight + 25;

                Raylib_cs.Rectangle Positions = new((int)x, (int)y, (int)CellWidth, (int)CellHeight);

                PositionsList.Add(Positions);
            }
        }
    }



    for (int i = 0; i < PositionsList.Count;)
    {
        int index = random.Next(0, PositionsList.Count);

        RandomizedPositions.Add(PositionsList[index]);
        PositionsList.RemoveAt(index);

    }
}

