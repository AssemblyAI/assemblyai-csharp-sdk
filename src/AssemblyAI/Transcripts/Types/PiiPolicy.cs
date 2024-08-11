using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AssemblyAI.Core;

#nullable enable

namespace AssemblyAI.Transcripts;

[JsonConverter(typeof(StringEnumSerializer<PiiPolicy>))]
public enum PiiPolicy
{
    [EnumMember(Value = "account_number")]
    AccountNumber,

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

    [EnumMember(Value = "date_interval")]
    DateInterval,

    [EnumMember(Value = "date_of_birth")]
    DateOfBirth,

    [EnumMember(Value = "drivers_license")]
    DriversLicense,

    [EnumMember(Value = "drug")]
    Drug,

    [EnumMember(Value = "duration")]
    Duration,

    [EnumMember(Value = "email_address")]
    EmailAddress,

    [EnumMember(Value = "event")]
    Event,

    [EnumMember(Value = "filename")]
    Filename,

    [EnumMember(Value = "gender_sexuality")]
    GenderSexuality,

    [EnumMember(Value = "healthcare_number")]
    HealthcareNumber,

    [EnumMember(Value = "injury")]
    Injury,

    [EnumMember(Value = "ip_address")]
    IpAddress,

    [EnumMember(Value = "language")]
    Language,

    [EnumMember(Value = "location")]
    Location,

    [EnumMember(Value = "marital_status")]
    MaritalStatus,

    [EnumMember(Value = "medical_condition")]
    MedicalCondition,

    [EnumMember(Value = "medical_process")]
    MedicalProcess,

    [EnumMember(Value = "money_amount")]
    MoneyAmount,

    [EnumMember(Value = "nationality")]
    Nationality,

    [EnumMember(Value = "number_sequence")]
    NumberSequence,

    [EnumMember(Value = "occupation")]
    Occupation,

    [EnumMember(Value = "organization")]
    Organization,

    [EnumMember(Value = "passport_number")]
    PassportNumber,

    [EnumMember(Value = "password")]
    Password,

    [EnumMember(Value = "person_age")]
    PersonAge,

    [EnumMember(Value = "person_name")]
    PersonName,

    [EnumMember(Value = "phone_number")]
    PhoneNumber,

    [EnumMember(Value = "physical_attribute")]
    PhysicalAttribute,

    [EnumMember(Value = "political_affiliation")]
    PoliticalAffiliation,

    [EnumMember(Value = "religion")]
    Religion,

    [EnumMember(Value = "statistics")]
    Statistics,

    [EnumMember(Value = "time")]
    Time,

    [EnumMember(Value = "url")]
    Url,

    [EnumMember(Value = "us_social_security_number")]
    UsSocialSecurityNumber,

    [EnumMember(Value = "username")]
    Username,

    [EnumMember(Value = "vehicle_id")]
    VehicleId,

    [EnumMember(Value = "zodiac_sign")]
    ZodiacSign
}
