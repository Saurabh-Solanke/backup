using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Question
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string QuestionText { get; set; }

    [Required]
    public List<string> Options { get; set; }

    [Required]
    public int Answer { get; set; }
}
