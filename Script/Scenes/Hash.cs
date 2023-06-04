using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace HashGame
{
    public class Hash_Scene : Scene
    {
        public class Block
        {
            public int isX;
            public int[] position = new int[2];
            public string Paint
            {
                get
                {
                    string p = "";
                    if (isX == 0) p = "X";
                    else p = "O";
                    return p;
                }
            }
        }

        private bool isX;
        private bool isEnd;
        public string Round { get => isX ? "X" : "O"; }
        Block[] blocks = new Block[9];

        int distance = 50;
        int height = 150;

        public override void Initialize()
        {
            Button song = new("Music: ON", 25, Color.White, new(() => { return scene.Graphics.PreferredBackBufferWidth - 75; }), new(() => { return 25; }), Color.White, 3, true, 0, 0, new(() => { }));
            song.action = new(() =>
            {
                scene.Play = !scene.Play;
                song.text = $"Music: {scene.issong}";
            });

            List<Button> buttons = new()
            {
                new("Exit", 25, Color.White, new(() => { return scene.Graphics.PreferredBackBufferWidth - 75; }), new(() => { return 70; }), Color.White, 3, true, 0, 0, new(() => {scene.SwitchScene(new Title_Scene());})),
                song,
            };


            int column = -1;
            int line = -1;
            for (int i = 0; i < 9; i++)
            {
                if (line == 2)
                {
                    column++;
                    line = -1;
                }
                blocks[i] = new();
                blocks[i].position[0] = line;
                blocks[i].position[1] = column;
                blocks[i].isX = -1;
                line++;
            }
            foreach (var block in blocks)
            {
                Button b;
                b = new("", 110, Color.White, new(() => { return (int)(scene.Graphics.PreferredBackBufferWidth / 2 - (102 * block.position[1])); }), new(() => { return (int)(scene.Graphics.PreferredBackBufferHeight / 2 + (102 * block.position[0])); }), Color.White, 0, false, 92, 92, new(() => { }));
                b.action = new(() =>
                {
                    if (block.isX == -1 && !isEnd)
                    {
                        block.isX = isX ? 0 : 1;
                        b.isMeassureString = true;
                        b.text = block.Paint;
                        CheckWinner();
                        isX = !isX;
                    }
                });
                buttons.Add(b);
            }

            EngineButton(buttons);
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = new(scene.GraphicsDevice);

            spriteBatch.Begin(SpriteSortMode.BackToFront);
            // Linhas

            spriteBatch.DrawLine(scene.Graphics.PreferredBackBufferWidth / 2 - distance, scene.Graphics.PreferredBackBufferHeight / 2 - height, scene.Graphics.PreferredBackBufferWidth / 2 - distance, scene.Graphics.PreferredBackBufferHeight / 2 + height, Color.White, 10, 1);
            spriteBatch.DrawLine(scene.Graphics.PreferredBackBufferWidth / 2 + distance, scene.Graphics.PreferredBackBufferHeight / 2 - height, scene.Graphics.PreferredBackBufferWidth / 2 + distance, scene.Graphics.PreferredBackBufferHeight / 2 + height, Color.White, 10, 1);

            spriteBatch.DrawLine(scene.Graphics.PreferredBackBufferWidth / 2 - height, scene.Graphics.PreferredBackBufferHeight / 2 - distance, scene.Graphics.PreferredBackBufferWidth / 2 + height, scene.Graphics.PreferredBackBufferHeight / 2 - distance, Color.White, 10, 1);
            spriteBatch.DrawLine(scene.Graphics.PreferredBackBufferWidth / 2 - height, scene.Graphics.PreferredBackBufferHeight / 2 + distance, scene.Graphics.PreferredBackBufferWidth / 2 + height, scene.Graphics.PreferredBackBufferHeight / 2 + distance, Color.White, 10, 1);


            var round = new Text($"Round: {Round}", 50, Color.White, new(() => { return scene.Graphics.PreferredBackBufferWidth / 2 - 80; }), new(() => { return 15; }), Color.White, 0);
            round.Scene = scene;
            round.Draw(gameTime);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        void CheckWinner()
        {
            int point = 0;

            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0) point = 0;
                if (blocks[i].isX == (isX ? 0 : 1)) point++;
                if (point == 3) break;
            }
            if (point == 3) Winner();

            point = 0;
            int j = 0;

            for (int i = 0; i < 3; i++)
            {
                for (; ; )
                {
                    if (blocks[i + j].isX == (isX ? 0 : 1)) point++;
                    if (point == 3) break;
                    j = j + 3;
                    if (j > 8) break;
                }
                if (point == 3) break;
                point = 0;
                j = 0;
            }
            if (point == 3) Winner();

            point = 0;
            if (blocks[0].isX == (isX ? 0 : 1) && blocks[4].isX == (isX ? 0 : 1) && blocks[8].isX == (isX ? 0 : 1)) Winner();
            else if (blocks[2].isX == (isX ? 0 : 1) && blocks[4].isX == (isX ? 0 : 1) && blocks[6].isX == (isX ? 0 : 1)) Winner();

            foreach (var b in blocks)
            {
                if (b.isX != -1) point++;
                if (point == blocks.Length) Winner(true);
            }
        }
        void Winner(bool isDraw = false)
        {
            if (isDraw && !isEnd)
            {
                EngineText(new(){
                new Text($"Draw", 70, Color.White, new(() => { return scene.Graphics.PreferredBackBufferWidth - 200; }), new(() => { return scene.Graphics.PreferredBackBufferHeight / 2 - 30; }), Color.White, 0),
            });
            }
            else if(!isEnd) EngineText(new(){
                new Text($"Winner", 70, Color.White, new(() => { return scene.Graphics.PreferredBackBufferWidth - 215; }), new(() => { return scene.Graphics.PreferredBackBufferHeight / 2 - 60; }), Color.White, 0),
                new Text($"{Round}", 70, Color.White, new(() => { return scene.Graphics.PreferredBackBufferWidth - 145; }), new(() => { return scene.Graphics.PreferredBackBufferHeight / 2; }), Color.White, 0),
            });

            isEnd = true;
        }
    }
}