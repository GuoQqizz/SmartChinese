using AbhsChinese.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Common
{
    public class ComplexEntity<T1, T2, T3>
    {
        public ComplexEntity()
        {
            EnableAudit = true;
        }

        public ComplexEntity(bool enableAudit)
        {
            EnableAudit = enableAudit;
        }

        public bool EnableAudit
        {
            set; get;
        }

        private T1 obj1;
        public T1 Obj1
        {
            set
            {
                obj1 = value;
                if (EnableAudit && obj1 is IChangeTrack)
                {
                    ((IChangeTrack)(obj1)).EnableAudit();
                }
            }
            get
            {
                return obj1;
            }
        }

        private T2 obj2;
        public T2 Obj2
        {
            set
            {
                obj2 = value;
                if (EnableAudit && obj2 is IChangeTrack)
                {
                    ((IChangeTrack)(obj2)).EnableAudit();
                }
            }
            get
            {
                return obj2;
            }
        }

        private T3 obj3;
        public T3 Obj3
        {
            set
            {
                obj3 = value;
                if (EnableAudit && obj3 is IChangeTrack)
                {
                    ((IChangeTrack)(obj3)).EnableAudit();
                }
            }
            get
            {
                return obj3;
            }
        }
    }

    public class ComplexEntity<T1, T2>
    {
        public ComplexEntity()
        {
            EnableAudit = true;
        }

        public ComplexEntity(bool enableAudit)
        {
            EnableAudit = enableAudit;
        }

        public bool EnableAudit
        {
            set; get;
        }

        private T1 obj1;
        public T1 Obj1
        {
            set
            {
                obj1 = value;
                if (EnableAudit && obj1 is IChangeTrack)
                {
                    ((IChangeTrack)(obj1)).EnableAudit();
                }
            }
            get
            {
                return obj1;
            }
        }

        private T2 obj2;
        public T2 Obj2
        {
            set
            {
                obj2 = value;
                if (EnableAudit && obj2 is IChangeTrack)
                {
                    ((IChangeTrack)(obj2)).EnableAudit();
                }
            }
            get
            {
                return obj2;
            }
        }
    }
}
