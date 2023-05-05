using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastucture.DataAccess.Contexts.Comment
{
    public class CommentConfiguration : IEntityTypeConfiguration<Domain.Comment>
    {
        public void Configure(EntityTypeBuilder<Domain.Comment> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Text).HasMaxLength(512).IsRequired();
            //builder.HasOne(s => s.Sender).WithMany(s => s.SendedComments).HasForeignKey(s => s.SenderId);
            //builder.HasOne(s => s.User).WithMany(s => s.RecievedComments).HasForeignKey(s => s.UserId);
            

        }
    }
}
