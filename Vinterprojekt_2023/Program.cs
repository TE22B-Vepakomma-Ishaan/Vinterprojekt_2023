
using System.ComponentModel;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

Random generator = new();



Raylib.InitWindow(1300, 1000, "Ghost game thing");
Raylib.SetTargetFPS(60);


string screen = "menu";


Rectangle gameBorder = new(25, 25, 800, 800);
Rectangle ghostBorder = new(950, 30, 240, 240);


Vector2 border = new(gameBorder.x, gameBorder.y);
Rectangle character = new(400, 450, 35, 35);
Texture2D PlayerSprite = Raylib.LoadTexture("RedEye2.png");
Texture2D Tile = Raylib.LoadTexture("purple_tile2.png");





Vector2 movement = new(0, 0);
float speed = 7;
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


int n = 12;

List<Rectangle> CellHitbox = new();

List<Tuple<int, int>> PositionsList = new();

List<Rectangle> points = new();

List<Tuple<int, int>> RandomizedPositions = new();


// Raylib--------------------------------------------------------------------------------------------------------------------------------

while (!Raylib.WindowShouldClose())
{

    Raylib.BeginDrawing();

    // Movement---------------------------------------------------------------------------------------------------------------

    movement = Vector2.Zero;



    if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
    {

        if (!Collision(CellHitbox, character.x + speed, character.y))
        {
            character.x += speed;
        }
    }
    else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
    {
        if (!Collision(CellHitbox, character.x - speed, character.y))
        {
            character.x -= speed;
        }
    }

    if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
    {
        if (!Collision(CellHitbox, character.x, character.y + speed))
        {
            character.y += speed;
        }
    }
    else if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
    {
        if (!Collision(CellHitbox, character.x, character.y - speed))
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
        Raylib.DrawRectangleLinesEx(ghostBorder, 5, Color.PURPLE);





        Raylib.DrawRectangleRec(character, Color.BLANK);
        Raylib.DrawTexture(PlayerSprite, (int)character.x, (int)character.y, Color.PURPLE);
        // Raylib.DrawText($"{character.x}{character.y}", 1000, 800, 20, Color.WHITE);

        GameScreen(PlayerSprite, Tile, character, ScreenWidth, ScreenHeight, GridSize, Level, CellHitbox, PositionsList);
        Pointsystem(Level, n, PositionsList, points, RandomizedPositions);
       
        foreach(var Point in RandomizedPositions)
        {
            int pointX = Point.Item1;
            int pointY = Point.Item2;

            Raylib.DrawRectangle(pointX, pointY, 20, 20, Raylib_cs.Color.GOLD);
        }
        Raylib.DrawFPS(1000, 500);


    }









    Raylib.EndDrawing();

}




// Functions-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


static void GameScreen(Texture2D PlayerSprite, Texture2D Tile, Rectangle character, int ScreenWidth, int ScreenHeight, int GridSize, int[,] Level, List<Rectangle> CellHitbox, List<Tuple<int, int>> PositionsList)
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

            if (Level[i, j] == 0)
            {
                Tuple<int, int> Position = new Tuple<int, int>(i, j);
                PositionsList.Add(Position);

            }
        }
    }
    // make better solution if time
    foreach (var cell in CellHitbox.Take(162))
    {
        Raylib.DrawRectangleRec(cell, Color.BLANK);
        Raylib.DrawTexture(Tile, (int)cell.x, (int)cell.y, Color.DARKPURPLE);
    }


}




static bool Collision(List<Rectangle> CellHitbox, float x, float y)
{
    Rectangle character = new Rectangle(x, y, 35, 35);

    foreach (var cell in CellHitbox)
    {
        if (Raylib.CheckCollisionRecs(character, cell))
        {
            return true;
        }
    }

    return false;
}



static void Pointsystem(int[,] Level, int n, List<Tuple<int, int>> PositionsList, List<Rectangle> points, List<Tuple<int, int>> RandomizedPositions)
{
    Random random = new();

    for (int i = 0; i < Level.GetLength(0); i++)
    {
        for (int j = 0; j < Level.GetLength(1); j++)
        {

            if (Level[i, j] == 0)
            {
                Tuple<int, int> Position = new Tuple<int, int>(i, j);
                PositionsList.Add(Position);
                int index = random.Next(0, PositionsList.Count);
                RandomizedPositions.Add(PositionsList[index]);


                foreach (var position in RandomizedPositions.Take(n))
                {

                    float PointWidth = 20;
                    float PointHeight = 20;
                    float x = position.Item2 * PointWidth;
                    float y = position.Item1 * PointHeight;

                    Rectangle point = new Rectangle(x, y, PointWidth, PointHeight);
                    points.Add(point);
                }
            }

        }
    }
}

