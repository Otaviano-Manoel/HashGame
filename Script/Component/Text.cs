using System.Collections;
using System.Collections.Generic;
using System.IO;
using HashGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using SpriteFontPlus;

public class Text : IDraw
{
    private Manager_Scene scene;
    public Manager_Scene Scene{set{
        scene = value;
        font = GetFontes(size);
    }}

    //Text
    public string text;
    public SpriteFont font;
    public Color text_Color;
    public int size;


    //Field
    public bool isMeassureString;
    public RectangleF field = new();
    public Color field_Color;
    public float thickness;

    public delegate int PositionX();
    public delegate int PositionY();

    public PositionX x;
    public PositionY y;
    public int height;
    public int width;

    public Text(string text, int size, Color tColor, PositionX x, PositionY y, Color rColor, float thickness, bool isMeassureString = false, int height = 0, int width = 0)
    {
        this.x = x;
        this.y = y;
        this.isMeassureString = isMeassureString;
        this.width = width;
        this. height = height;
        this.size = size;
        text_Color = tColor;
        this.text = text;
        field_Color = rColor;
        this.thickness = thickness;
    }

    public SpriteFont GetFontes(int size)
    {
        var font = TtfFontBaker.Bake(File.ReadAllBytes(@"./Content/Font.ttf"), size, 1024, 1024,
                new[] { CharacterRange.BasicLatin, CharacterRange.Latin1Supplement, CharacterRange.LatinExtendedA, CharacterRange.Cyrillic });

        return font.CreateSpriteFont(scene.GraphicsDevice);
    }

    public void Draw(GameTime gameTime)
    {
        field.X = x() - field.Size.Width / 2;
        field.Y = y() - field.Size.Height / 2;
        if (isMeassureString)
        {
            field.Width = font.MeasureString(text).X + (thickness * 2) + 6;
            field.Height = 6 + font.MeasureString(text).Y + thickness;
        }
        else{
            field.Width = width;
            field.Height = height;
        }
        SpriteBatch batch = new(scene.GraphicsDevice);
        batch.Begin(SpriteSortMode.BackToFront);
        batch.DrawRectangle(field, field_Color, thickness, 1);
        batch.DrawString(font, text, new(field.Position.X + thickness + 3, field.Position.Y + 3), text_Color);
        batch.End();
    }
}