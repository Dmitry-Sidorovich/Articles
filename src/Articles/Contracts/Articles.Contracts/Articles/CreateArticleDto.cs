namespace Articles.Contracts.Articles;

public class CreateArticleDto
{
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public DateTime CreatedAt { get; set; } // сервер сам ставит, на входе не нужны, клиент их не передает?
    //
    public string UserName { get; set; }
}