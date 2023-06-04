using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IDraw
{
    public abstract void Draw(GameTime gameTime);
}

public interface IUpdate
{
    public abstract void Update(GameTime gameTime);
}