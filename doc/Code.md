# Code

## Code anotations

I left some **"NOTE:"** comments in the code. Of something you might think is weird. Look for the string **"NOTE:"** to find them.  
If you look for **"TODO:"** you'll find a lot of temporary shit which I did in the faster way and I have to finish/refactor/delete.

## Convention

I use this [Style Guide](https://github.com/kenny-reyes/Guides/blob/master/Documents/CSharpStyleGuide.md). And powered by resharper.

## Tools

I had use JetBrains Rider for developing this, I like it. This doesn't mean I can't use VS 2019, more, I used also VS 2019 community and VS Code for developing. I like to try everything before having an opinion.
I also used postman, fiddler, SQL Management, DataGrip, SmartGit (I use mainly the cmd), and Docker Desktop.

## Domain changes

I added email to user because I wanted to add a nice validation with a regex.  
I also added Gender because I wanted to use the type-safe enum, very useful in sqlserver for saving enums. PostgreSQL for example don't need this because it has the type enum build-in.

```C#
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public DateTime Birthdate { get; private set; }
        public int GenderId { get; private set; }
        public Gender Gender { get; private set; }
    }
```

[Go back](Index.md)
