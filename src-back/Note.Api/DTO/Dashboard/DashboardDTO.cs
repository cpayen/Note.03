using Note.Api.DTO.Column;
using System.Collections.Generic;

namespace Note.Api.DTO.Dashboard
{
    public class DashboardDTO : DashboardLightDTO
    {
        public List<ColumnDTO> Columns;
    }
}
