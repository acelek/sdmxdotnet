﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;

namespace SDMX.Parsers
{
    internal class ContactMap : ClassMap<Contact>
    {
        Contact contact = new Contact();
        
        public ContactMap()
        {
            MapCollection(o => o.Name).ToElement("Name", false)
                .Set(v => contact.Name.Add(v))
                .ClassMap(() => new InternationalStringMap());

            MapCollection(o => o.Department).ToElement("Department", false)
                .Set(v => contact.Department.Add(v))
                .ClassMap(() => new InternationalStringMap());

            MapCollection(o => o.Role).ToElement("Role", false)
                .Set(v => contact.Role.Add(v))
                .ClassMap(() => new InternationalStringMap());

            MapCollection(o => o.TelephoneList).ToSimpleElement("Telephone", false)
                .Set(v => contact.TelephoneList.Add(v))
                .Converter(new StringConverter());

            MapCollection(o => o.FaxList).ToSimpleElement("Fax", false)
                .Set(v => contact.FaxList.Add(v))
                .Converter(new StringConverter());

            MapCollection(o => o.X400List).ToSimpleElement("X400", false)
                .Set(v => contact.X400List.Add(v))
                .Converter(new StringConverter());

            MapCollection(o => o.UriList).ToSimpleElement("URI", false)
                .Set(v => contact.UriList.Add(v))
                .Converter(new UriConverter());

            MapCollection(o => o.EmailList).ToSimpleElement("Email", false)
                .Set(v => contact.EmailList.Add(v))
                .Converter(new StringConverter());
        }

        protected override Contact Return()
        {
            return contact;
        }
    }
}
