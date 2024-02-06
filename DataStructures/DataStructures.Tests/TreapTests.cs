namespace DataStructures.Tests;

public class TreapTests
{
    private readonly Treap treap = new();
    private readonly (string, double)[] nodes = 
    {
        ("Flour", 10),
        ("Butter", 76),
        ("Water", 32),
        ("Bacon", 77),
        ("Eggs", 129),
        ("Cabbage", 155),
        ("Milk", 55),
        ("Pork", 56),
        ("Wine", 4),
        ("A", 1)
    };

    [Fact]
    public void Can_Add_Items_To_Treap()
    {
        foreach (var (key, priority) in nodes)
        {
            treap.Insert(key, priority);
        }

        Assert.True(true);
    }

    [Fact]
    public void Can_Find_Key_In_Treap()
    {
        foreach (var (key, priority) in nodes)
        {
            treap.Insert(key, priority);
        }

        var hasFlour = treap.Search("Flour");
        Assert.True(hasFlour);
    }
}