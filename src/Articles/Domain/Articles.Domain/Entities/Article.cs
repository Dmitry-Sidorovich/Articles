using Articles.Domain.Base;

namespace Articles.Domain.Entities;

public class Article : EntityBase
{
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public User User { get; set; }
    
    // EF сможет сетить через private set
    // public string Title { get; private set; } = default!;
    // public string Description { get; private set; } = default!;
    //
    // Пустой конструктор для EF
    // private Article() { }

    // Фабричный способ создания (потом добавим правила)
    // public static Article Create(string title, string description, DateTimeOffset now, Guid? id = null)
    // { ... }
}