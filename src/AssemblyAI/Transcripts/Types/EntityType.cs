using System.Runtime.Serialization;

#nullable enable

namespace AssemblyAI;

public enum EntityType
{
    [EnumMember(Value = "banking_information")]
    BankingInformation,

    [EnumMember(Value = "blood_type")]
    BloodType,

    [EnumMember(Value = "credit_card_cvv")]
    CreditCardCvv,

    [EnumMember(Value = "credit_card_expiration")]
    CreditCardExpiration,

    [EnumMember(Value = "credit_card_number")]
    CreditCardNumber,

    [EnumMember(Value = "date")]
    Date,

    [EnumMember(Value = "date_of_birth")]
    DateOfBirth,

    [EnumMember(Value = "drivers_license")]
    DriversLicense,

    [EnumMember(Value = "drug")]
    Drug,

    [EnumMember(Value = "email_address")]
    EmailAddress,

    [EnumMember(Value = "event")]
    Event,

    [EnumMember(Value = "injury")]
    Injury,

    [EnumMember(Value = "language")]
    Language,

    [EnumMember(Value = "location")]
    Location,

    [EnumMember(Value = "medical_condition")]
    MedicalCondition,

    [EnumMember(Value = "medical_process")]
    MedicalProcess,

    [EnumMember(Value = "money_amount")]
    MoneyAmount,

    [EnumMember(Value = "nationality")]
    Nationality,

    [EnumMember(Value = "occupation")]
    Occupation,

    [EnumMember(Value = "organization")]
    Organization,

    [EnumMember(Value = "password")]
    Password,

    [EnumMember(Value = "person_age")]
    PersonAge,

    [EnumMember(Value = "person_name")]
    PersonName,

    [EnumMember(Value = "phone_number")]
    PhoneNumber,

    [EnumMember(Value = "political_affiliation")]
    PoliticalAffiliation,

    [EnumMember(Value = "religion")]
    Religion,

    [EnumMember(Value = "time")]
    Time,

    [EnumMember(Value = "url")]
    Url,

    [EnumMember(Value = "us_social_security_number")]
    UsSocialSecurityNumber
}
