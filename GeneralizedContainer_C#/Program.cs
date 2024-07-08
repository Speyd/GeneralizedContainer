using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;



try
{
    Container<Pups> containerPups = new Container<Pups>();
    containerPups.addElement(new Pups());
    Console.WriteLine(containerPups.render());

    Console.WriteLine();

    Container<int> container = new Container<int>();
    container.addElement(1);
    container.addElement(1000);
    container.addElement(-1);
    Console.WriteLine(container.render());
}
catch(Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}


class Pups { }

class Container<T>
{
    public int Count {  get; set; }
    private List<T> list = new List<T>();

    public Container(List<T>? list = null)
    {
        if(list is not null)
            this.list = list;
    }

    private bool CanUseToString()
    {
        Type type = typeof(T);

        MethodInfo? toStringMethod = type.GetMethod("ToString", Type.EmptyTypes);

        return toStringMethod != null && toStringMethod.DeclaringType != typeof(object);
    }

    public void addElement(T element)
    {
        list.Add(element);
        Count++;
    }

    public void removeElement(T element)
    {
        list.Remove(element);
        Count--;
    }

    public T getElement(int index)
    {
        if (index < 0 || index >= list.Count)
            throw new ArgumentOutOfRangeException("index");

        return list[index];
    }

    public string render() 
    {
        if (CanUseToString() == false)
            return "Error render!\nT cannot convert to string!";
        else if (Count == 0)
            return "Empty Container";


        string renderText = "";

        foreach (T element in list)
            renderText += $"{element?.ToString()}\n";

        return renderText;
    }

}