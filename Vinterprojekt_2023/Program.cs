
using System.ComponentModel;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
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
Raylib_cs.Rectangle doorHitbox = new(429, 465, 2, 10);


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
int score = 0;


List<Raylib_cs.Rectangle> CellHitbox = new();

List<Raylib_cs.Rectangle> PositionsList = new();

List<Raylib_cs.Rectangle> RandomizedPositions = new();

List<Raylib_cs.Rectangle> CoinSatchel = new();










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


        CoinRandomize(random, ScreenWidth, ScreenHeight, GridSize, Level, PositionsList, RandomizedPositions, CoinSatchel, n);


        for (int i = 0; i < CoinSatchel.Count ; i++)
        {
            Raylib.DrawRectangleRec(CoinSatchel[i], Raylib_cs.Color.BLANK);
            Raylib.DrawTexture(CoinSprite, (int)CoinSatchel[i].x, (int)CoinSatchel[i].y, Raylib_cs.Color.GOLD);

        }

        for (int i = CoinSatchel.Count -1 ; i >= 0; i--)
        {
            if (Raylib.CheckCollisionRecs(character, CoinSatchel[i]))
            {
                Console.WriteLine($"Collision detected with coin at index {i}");
                CoinSatchel.RemoveAt(i);
                score++;
                break;
            }
        }



        Raylib.DrawRectangleRec(door, Raylib_cs.Color.GRAY);
        Raylib.DrawRectangleRec(doorHitbox, Raylib_cs.Color.BLANK);

        Raylib.DrawRectangleRec(character, Raylib_cs.Color.BLANK);
        Raylib.DrawTexture(PlayerSprite, (int)character.x, (int)character.y, Raylib_cs.Color.PURPLE);


        Raylib.DrawText($"{character.x}  {character.y}", 1000, 800, 30, Raylib_cs.Color.WHITE);
        Raylib.DrawText($"Score: {score}", 1000, 200, 40, Raylib_cs.Color.WHITE);
        Raylib.DrawText($" Coins in CoinSatchel list: {CoinSatchel.Count}", 800, 400, 25, Raylib_cs.Color.WHITE);
        Raylib.DrawFPS(1000, 500);


        if (score >= n && Raylib.CheckCollisionRecs(character, doorHitbox))
        {
            screen = "end";
        }
    }


    if(screen == "end")
    {
        Raylib.ClearBackground(Raylib_cs.Color.BLACK);
        Raylib.DrawText("LEVEL COMPLETE!", 375, 500, 50, Raylib_cs.Color.WHITE);
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



static void CoinRandomize(Random random, int ScreenWidth, int ScreenHeight, int GridSize, int[,] Level, List<Raylib_cs.Rectangle> PositionsList, List<Raylib_cs.Rectangle> RandomizedPositions, List<Raylib_cs.Rectangle> CoinSatchel, int n)
{
    for (int i = 0; i < Level.GetLength(0); i++)
    {
        for (int j = 0; j < Level.GetLength(1); j++)
        {
            if (Level[i, j] == 0 && (i != 8 || j != 8) && (i != 9 || j != 8) && (i != 10 || j != 8))
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

    foreach (var Position in RandomizedPositions.Take(n))
    {


        float CoinX = Position.x + 12;
        float CoinY = Position.y + 12;
        Raylib_cs.Rectangle coin = new(CoinX, CoinY, 20, 20);

        if (CoinSatchel.Count < n )
        {
            CoinSatchel.Add(coin);
        }
    }
}

