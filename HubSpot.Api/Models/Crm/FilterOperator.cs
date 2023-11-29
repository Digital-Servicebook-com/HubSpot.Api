using System.Runtime.Serialization;

namespace HubSpot.Api.Models;

public enum FilterOperator
{
    [EnumMember(Value = "EQ")]
	Eq,
    [EnumMember(Value = "NEQ")]
	Neq,
    [EnumMember(Value = "LT")]
	Lt,
    [EnumMember(Value = "LTE")]
	Lte,
    [EnumMember(Value = "GT")]
	Gt,
    [EnumMember(Value = "GTE")]
	Gte,
    [EnumMember(Value = "BETWEEN")]
	Between,
    [EnumMember(Value = "IN")]
	In,
    [EnumMember(Value = "NOT_IN")]
	NotIn,
    [EnumMember(Value = "HAS_PROPERTY")]
	HasProperty,
    [EnumMember(Value = "NOT_HAS_PROPERTY")]
	NotHasProperty,
    [EnumMember(Value = "CONTAINS_TOKEN")]
	ContainsToken,
    [EnumMember(Value = "NOT_CONTAINS_TOKEN")]
	NotContainsToken,
}