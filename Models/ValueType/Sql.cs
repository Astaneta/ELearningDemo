namespace ELearningDemo.Models.ValueType
{
    public class Sql
    {
        public string Value { get; }

        private Sql(string value)
        {
            Value = value;
        }

        public static explicit operator Sql(string value) => new Sql(value);

        public override string ToString()
        {
            return this.Value;
        }
    }
}