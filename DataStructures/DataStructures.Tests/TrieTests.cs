namespace DataStructures.Tests;

public class TrieTests
{
    private readonly Trie trie = new();
    private readonly string[] words = 
    {
        "Flour",
        "Butter",
        "Water",
        "Bacon",
        "Eggs",
        "Cabbage",
        "Milk",
        "Pork", 
        "Wine",
        "A",
    };
    
    [Fact]
    public void Can_Insert_Into_Trie()
    {
        foreach (var word in words)
        {
            trie.Insert(word);
        }

        Assert.True(true);
    }

    [Fact]
    public void Can_Remove_And_Prune()
    {
        const string removeWord = "Wine";
        foreach (var word in words)
        {
            trie.Insert(word);
        }

        trie.Remove(removeWord);

        Assert.False(trie.Contains(removeWord));
        Assert.All(words.Where(w => w != removeWord), s => trie.Contains(s));
    }
}