namespace Api.Models.Responses
{
    public class Todo
    {
        public string Title { get; }
        public bool Completed { get; }

        public Todo(string title,
            bool completed)
        {
            Title = title;
            Completed = completed;
        }
    }
}
