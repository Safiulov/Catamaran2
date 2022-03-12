using System.Collections.Generic;
namespace Catamaran2
{
    public class BoatComparer : IComparer<Iboat>
    {
        public int Compare(Iboat x, Iboat y)
        {
             if (x!=null && y!=null)
            {
                if (x is Лодка && y is ЛодкаКатамаран)
                {
                    return 1;
                }
                if (x is ЛодкаКатамаран && y is Лодка)
                {
                    return -1;
                }
                if (x is Лодка && y is Лодка)
                {
                    return (x is Лодка).CompareTo(y is Лодка);
                }
                if (x is ЛодкаКатамаран && y is ЛодкаКатамаран)
                {
                    return (y is ЛодкаКатамаран).CompareTo(y is ЛодкаКатамаран);
                }
            }
            return 0;
        }

    
    private int ComparerЛодка(Лодка x, Лодка y)
        {
            if (y == null)
            { 
                return 1;
            }
            if (x.Speed != y.Speed)
            {
                return x.Speed.CompareTo(y.Speed);
            }
            if (x.Weight != y.Weight)
            {
                return x.Weight.CompareTo(y.Weight);
            }
            if (x.BodyColor != y.BodyColor)
            {
                x.BodyColor.Name.CompareTo(y.BodyColor.Name);
            }
            return 0;

        }
        private int ComparerЛодкаКатамаран(ЛодкаКатамаран x, ЛодкаКатамаран y)
        {
            var res = (x is Лодка).CompareTo(y is Лодка);
            if (res != 0)
            {
                return res;
            }
            if (x.DopColor != y.DopColor)
            {
                x.DopColor.Name.CompareTo(y.DopColor.Name);
            }
            if (x.Leftpop != y.Leftpop)
            {
                return x.Leftpop.CompareTo(y.Leftpop);
            }
            if (x.Rightpop != y.Rightpop)
            {
                return x.Rightpop.CompareTo(y.Rightpop);
            }
            if (x.Parus != y.Parus)
            {
                return x.Parus.CompareTo(y.Parus);
            }
            return 0;
        }
    }
}