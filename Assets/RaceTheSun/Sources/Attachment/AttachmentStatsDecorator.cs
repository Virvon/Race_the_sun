using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Attachment
{
    public abstract class AttachmentStatsDecorator : IAttachmentStatsProvider
    {
        protected readonly IAttachmentStatsProvider WrappedEntity;

        protected AttachmentStatsDecorator(IAttachmentStatsProvider wrappedEntity) =>
            WrappedEntity = wrappedEntity;

        public AttachmentStats GetStats() =>
            GetStatsInternal();

        protected abstract AttachmentStats GetStatsInternal();
    }
}
