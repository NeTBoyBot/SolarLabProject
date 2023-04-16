using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastucture.DataAccess.Contexts.Message
{
    public class MessageConfiguration : IEntityTypeConfiguration<Domain.Message>
    {
        public void Configure(EntityTypeBuilder<Domain.Message> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasOne(m => m.Sender).WithMany(m => m.SendedMessages).HasForeignKey(m => m.SenderId);
            builder.HasOne(m => m.Reciever).WithMany(m => m.RecievedMessages).HasForeignKey(m => m.RecieverId);
        }
    }
}
