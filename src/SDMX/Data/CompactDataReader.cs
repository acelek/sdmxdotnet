using System.Collections.Generic;
using System.Xml;
using System;

namespace SDMX
{
    public class CompactDataReader : DataReader
    {
        public CompactDataReader(XmlReader xmlReader, KeyFamily keyFamily)
            : base(xmlReader, keyFamily)
        { }

        public override bool Read()
        {
            CheckDisposed();

            ClearErrors();

            while (XmlReader.Read())
            {
                if (!XmlReader.IsStartElement())
                    continue;

                var group = KeyFamily.Groups.Find(XmlReader.LocalName);

                if (group != null)
                {
                    NewGroupValues();
                    while (XmlReader.MoveToNextAttribute())
                    {
                        ReadValue((n, v) => SetGroup(group, n, v));
                    }

                    ValidateGroup(group);
                }
                else if (XmlReader.LocalName == "Series")
                {
                    ClearSeries();
                    while (XmlReader.MoveToNextAttribute())
                    {
                        ReadValue((n, v) => SetSeries(n, v));
                    }

                    ValidateSeries();
                }
                else if (XmlReader.LocalName == "Obs")
                {
                    ClearObs();

                    while (XmlReader.MoveToNextAttribute())
                    {
                        ReadValue((n, v) => SetObs(n, v));
                    }

                    ValidateObs();
                    SetRecord();

                    return true;
                }
            }

            return false;
        }

        void ReadValue(Action<string, string> set)
        {
            string name = XmlReader.LocalName;
            string value = XmlReader.Value;
            //string startTime = XmlReader.GetAttribute("startTime");
            bool error = false;
            if (IsNullOrEmpty(value))
            {
                AddValidationError("Value for attribute '{0}' is missing.", name);
                error = true;
            }

            if (!error)
            {
                set(name, value);
            }
        }
    }
}
