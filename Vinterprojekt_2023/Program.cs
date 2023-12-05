
using System.Numerics;
using System.Runtime.CompilerServices;
using Raylib_cs;

Random generator = new();



Raylib.InitWindow(1300, 1000, "Ghost");
Raylib.SetTargetFPS(60);


string screen = "menu";


Rectangle gameBorder = new(25, 25, 800, 800);
Rectangle ghostBorder = new(950, 30, 240, 240);


Vector2 border = new(gameBorder.x, gameBorder.y);
Rectangle character = new(400, 400, 40, 40);
Texture2D PlayerSprite = Raylib.LoadTexture("GreenBall.png");


Vector2 movement = new(0, 0);
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

int characterX = 1;
int characterY = 1;

float characterCellWidth = 40;
float characterCellHeight = 40;
float characterDrawX = characterX * characterCellWidth;
float characterDrawY = characterY * characterCellHeight;


while (!Raylib.WindowShouldClose())
{

    Raylib.BeginDrawing();

    // Movement---------------------------------------------------------------------------------------------------------------

    movement = Vector2.Zero;

    if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
    {
        movement.X += 2;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
    {
        movement.X -= 2;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
    {
        movement.Y -= 2;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
    {
        movement.Y += 2;
    }

    if (movement.Length() > 0)
    {
        movement = Vector2.Normalize(movement) * speed;
    }

    characterDrawX += movement.X;
    characterDrawY += movement.Y;

    // ------------------Micke-----------------------
    character.x = Math.Clamp(character.x, 25, 699);
    character.y = Math.Clamp(character.y, 25, 699);
    // ------------------Micke----------------------- 


    if (Raylib.IsKeyDown(KeyboardKey.KEY_D) && characterX < GridSize - 1 && Level[characterY, characterX + 1] == 0)
    {
        characterX++;
    }
    else if (Raylib.IsKeyDown(KeyboardKey.KEY_A) && characterX > 0 && Level[characterY, characterX - 1] == 0)
    {
        characterX--;
    }

    if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && characterY < GridSize - 1 && Level[characterY + 1, characterX] == 0)
    {
        characterY++;
    }
    else if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && characterY > 0 && Level[characterY - 1, characterX] == 0)
    {
        characterY--;
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
        Raylib.DrawText($"{characterDrawX}{characterDrawY}", 1000, 800, 20, Color.WHITE);

        GameScreen(PlayerSprite, ScreenWidth, ScreenHeight, GridSize, Level, characterCellWidth, characterCellHeight, characterDrawX, characterDrawY);



    }









    Raylib.EndDrawing();

}



static void GameScreen(Texture2D PlayerSprite, int ScreenWidth, int ScreenHeight, int GridSize, int[,] Level, float characterCellWidth, float characterCellHeight, float characterDrawX, float characterDrawY)
{
    for (int i = 0; i < Level.GetLength(0); i++)
    {
        for (int j = 0; j < Level.GetLength(1); j++)
        {
            if (Level[i, j] == 1)
            {
                // Calculate the position based on the 17x17 grid and the 800x800 window size
                float CellWidth = ScreenWidth / (float)GridSize;
                float CellHeight = ScreenHeight / (float)GridSize;
                float x = j * CellWidth + 25;
                float y = i * CellHeight + 25;

                Raylib.DrawRectangle((int)x, (int)y, (int)CellWidth, (int)CellHeight, Color.BLUE);
            }
            // Add more conditions to draw other elements (dots, Pac-Man, ghosts, etc.)
        }
    }


    Raylib.DrawRectangle((int)characterDrawX, (int)characterDrawY, (int)characterCellWidth, (int)characterCellHeight, Color.YELLOW);
    Raylib.DrawTexture(PlayerSprite, (int)characterDrawX, (int)characterDrawY, Color.PURPLE);
}

