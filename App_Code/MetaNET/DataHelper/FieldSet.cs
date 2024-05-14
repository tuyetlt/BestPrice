namespace MetaNET.DataHelper
{
    using System;

    public class FieldSet
    {
        private int _Length;
        private string _Name;
        private FieldType _Type;
        private string _Value;

        public FieldSet()
        {
            this._Type = FieldType.NO_STRING;
            this._Length = 0;
        }

        public FieldSet(string Name, object Value, FieldType Type)
        {
            this._Type = FieldType.NO_STRING;
            this._Length = 0;
            this.Name = Name;
            this.Value = Value.ToString();
            this.Type = Type;
            this.Length = 0;
        }

        public FieldSet(string Name, object Value, FieldType Type, int Length)
        {
            this._Type = FieldType.NO_STRING;
            this._Length = 0;
            this.Name = Name;
            this.Value = Value.ToString();
            this.Type = Type;
            this.Length = Length;
        }

        public int Length
        {
            get
            {
                return this._Length;
            }
            set
            {
                this._Length = (value > 0) ? value : 0;
            }
        }

        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        public FieldType Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }

        public string Value
        {
            get
            {
                string str = this._Value;
                if (this._Type != FieldType.NO_STRING)
                {
                    if ((this._Length != 0) && (this._Length < str.Length))
                    {
                        str = str.Substring(0, this._Length);
                    }
                    str = "'" + str.Replace("'", "''") + "'";
                    if (this._Type == FieldType.STRING_N)
                    {
                        str = "N" + str;
                    }
                }
                return str;
            }
            set
            {
                this._Value = value;
            }
        }

        public enum FieldType
        {
            STRING,
            STRING_N,
            NO_STRING
        }
    }
}

