using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BankApi.Models;

[Table("BankAccount")]
public class BankAccountModel
{
    [JsonIgnore]
    [Key]
    public int AccountId { get; set; }

    [Required]
    [StringLength(255)]
    public string? NameClient { get; set; }

    [Required]
    public bool PhisicalPersonAccount { get; set; }

    [Required]
    public bool JuridicPersonAccount { get; set; }

    [Required]
    [StringLength(255)]
    public string? PhoneClient { get; set; }

    [Required]
    [StringLength(255)]
    public string? DocumentClient { get; set; }

    [Required]
    [StringLength(255)]
    public string? EmailClient { get; set;}

    [JsonIgnore]
    [Column(TypeName = "decimal(18,4)")]
    public decimal BalanceClient { get; set; } 


}
