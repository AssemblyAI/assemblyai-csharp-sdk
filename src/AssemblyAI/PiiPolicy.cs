using System;

namespace AssemblyAI
{
    public class PiiPolicy
    {
        public static readonly PiiPolicy EmailAddress = new PiiPolicy(Value.EmailAddress, "email_address");

        public static readonly PiiPolicy Nationality = new PiiPolicy(Value.Nationality, "nationality");

        public static readonly PiiPolicy MedicalProcess = new PiiPolicy(Value.MedicalProcess, "medical_process");

        public static readonly PiiPolicy PersonAge = new PiiPolicy(Value.PersonAge, "person_age");

        public static readonly PiiPolicy Date = new PiiPolicy(Value.Date, "date");

        public static readonly PiiPolicy BankingInformation = new PiiPolicy(Value.BankingInformation, "banking_information");

        public static readonly PiiPolicy CreditCardCvv = new PiiPolicy(Value.CreditCardCVV, "credit_card_cvv");

        public static readonly PiiPolicy Occupation = new PiiPolicy(Value.Occupation, "occupation");

        public static readonly PiiPolicy Language = new PiiPolicy(Value.Language, "language");

        public static readonly PiiPolicy CreditCardExpiration = new PiiPolicy(Value.CreditCardExpiration, "credit_card_expiration");

        public static readonly PiiPolicy MoneyAmount = new PiiPolicy(Value.MoneyAmount, "money_amount");

        public static readonly PiiPolicy Drug = new PiiPolicy(Value.Drug, "drug");

        public static readonly PiiPolicy PhoneNumber = new PiiPolicy(Value.PhoneNumber, "phone_number");

        public static readonly PiiPolicy DateOfBirth = new PiiPolicy(Value.DateOfBirth, "date_of_birth");

        public static readonly PiiPolicy Injury = new PiiPolicy(Value.Injury, "injury");

        public static readonly PiiPolicy NumberSequence = new PiiPolicy(Value.NumberSequence, "number_sequence");

        public static readonly PiiPolicy PoliticalAffiliation = new PiiPolicy(Value.PoliticalAffiliation, "political_affiliation");

        public static readonly PiiPolicy Location = new PiiPolicy(Value.Location, "location");

        public static readonly PiiPolicy Event = new PiiPolicy(Value.Event, "event");

        public static readonly PiiPolicy PersonName = new PiiPolicy(Value.PersonName, "person_name");

        public static readonly PiiPolicy Organization = new PiiPolicy(Value.Organization, "organization");

        public static readonly PiiPolicy DriversLicense = new PiiPolicy(Value.DriversLicense, "drivers_license");

        public static readonly PiiPolicy MedicalCondition = new PiiPolicy(Value.MedicalCondition, "medical_condition");

        public static readonly PiiPolicy BloodType = new PiiPolicy(Value.BloodType, "blood_type");

        public static readonly PiiPolicy CreditCardNumber = new PiiPolicy(Value.CreditCardNumber, "credit_card_number");

        public static readonly PiiPolicy Religion = new PiiPolicy(Value.Religion, "religion");

        public static readonly PiiPolicy USSocialSecurityNumber = new PiiPolicy(Value.USSocialSecurityNumber, "us_social_security_number");

        
        private readonly Value _value;
        private readonly String _raw;

        private PiiPolicy(Value value, String raw) {
        this._value = value;
        this._raw = raw;
    }
    
        public enum Value {
            MedicalProcess,
            MedicalCondition,
            BloodType,
            Drug,
            Injury,
            NumberSequence,
            EmailAddress,
            DateOfBirth,
            PhoneNumber,
            USSocialSecurityNumber,
            CreditCardNumber,
            CreditCardExpiration,
            CreditCardCVV,
            Date,
            Nationality,
            Event,
            Language,
            Location,
            MoneyAmount,
            PersonName,
            PersonAge,
            Organization,
            PoliticalAffiliation,
            Occupation,
            Religion,
            DriversLicense,
            BankingInformation,
            Unknown
        }
    }
}