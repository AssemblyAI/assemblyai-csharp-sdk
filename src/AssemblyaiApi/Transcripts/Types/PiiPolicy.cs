using System.Runtime.Serialization;

namespace AssemblyaiApi;

public enum PiiPolicy
{
    [EnumMember(Value = "medical_process")]
    MedicalProcess,

    [EnumMember(Value = "medical_condition")]
    MedicalCondition,

    [EnumMember(Value = "blood_type")]
    BloodType,

    [EnumMember(Value = "drug")]
    Drug,

    [EnumMember(Value = "injury")]
    Injury,

    [EnumMember(Value = "number_sequence")]
    NumberSequence,

    [EnumMember(Value = "email_address")]
    EmailAddress,

    [EnumMember(Value = "date_of_birth")]
    DateOfBirth,

    [EnumMember(Value = "phone_number")]
    PhoneNumber,

    [EnumMember(Value = "us_social_security_number")]
    UsSocialSecurityNumber,

    [EnumMember(Value = "credit_card_number")]
    CreditCardNumber,

    [EnumMember(Value = "credit_card_expiration")]
    CreditCardExpiration,

    [EnumMember(Value = "credit_card_cvv")]
    CreditCardCvv,

    [EnumMember(Value = "date")]
    Date,

    [EnumMember(Value = "nationality")]
    Nationality,

    [EnumMember(Value = "event")]
    Event,

    [EnumMember(Value = "language")]
    Language,

    [EnumMember(Value = "location")]
    Location,

    [EnumMember(Value = "money_amount")]
    MoneyAmount,

    [EnumMember(Value = "person_name")]
    PersonName,

    [EnumMember(Value = "person_age")]
    PersonAge,

    [EnumMember(Value = "organization")]
    Organization,

    [EnumMember(Value = "political_affiliation")]
    PoliticalAffiliation,

    [EnumMember(Value = "occupation")]
    Occupation,

    [EnumMember(Value = "religion")]
    Religion,

    [EnumMember(Value = "drivers_license")]
    DriversLicense,

    [EnumMember(Value = "banking_information")]
    BankingInformation
}
