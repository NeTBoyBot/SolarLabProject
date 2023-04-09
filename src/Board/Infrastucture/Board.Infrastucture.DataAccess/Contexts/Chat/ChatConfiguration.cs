using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastucture.DataAccess.Contexts.Chat
{
    public class ChatConfiguration : IEntityTypeConfiguration<Domain.Chat>
    {
        public void Configure(EntityTypeBuilder<Domain.Chat> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasOne(c => c.Creator).WithMany(cr => cr.Chats).HasForeignKey(c=>c.Id);
            
        }
    }
}
