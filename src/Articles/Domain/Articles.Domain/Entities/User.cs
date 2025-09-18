﻿using Articles.Domain.Base;

namespace Articles.Domain.Entities;

public class User : EntityBase
{
    public string FullName { get; set; }
    
    public ICollection<Article> Articles { get; set; } // навигационное свойство
}