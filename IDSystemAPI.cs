using Microsoft.AspNetCore.Mvc;
using IDSystemBusinessLogic;

namespace IDSystemWeb.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class IDSystemAPI : ControllerBase
    {


        [HttpGet]
        public ActionResult<IEnumerable<studentDTO>> getstudents()
        {


            var list = Checking.AdminListAll().Select(studentdto => new studentDTO{Id = studentdto.StudentId, Name = studentdto.Name});
            return Ok(list);


        }


        
        [HttpGet("{id}/schedule")]
        public ActionResult<schedDTO> getSchedd(string id)
        {


            if (!Checking.checkId(id))
                return NotFound($"Student '{id}' not found.");


            Checking.setCurrentID(id);
            var lines = Checking.getSchedule().Split('\n', System.StringSplitOptions.RemoveEmptyEntries);
            return Ok(new schedDTO
            {


                Id = id,
                Schedule = lines


            });


        }



        [HttpGet("{id}/record")]
        public ActionResult<recrdDTO> recordGet(string id)
        {


            if (!Checking.checkId(id))
                return NotFound($"Student '{id}' not found.");


            Checking.setCurrentID(id);
            var (lates, abs) = Checking.getRecord();
            return Ok(new recrdDTO
            {


                Id = id,
                Lates = lates,
                Absents = abs


            });


        }




        [HttpPost("{id}/inout")]
        public ActionResult<inOutDTO> InOut(string id)
        {


            if (!Checking.checkId(id))
                return NotFound($"Student '{id}' not found.");


            Checking.setCurrentID(id);
            var message = Checking.InOrOut();
            return Ok(new inOutDTO
            {


                Id = id,
                Message = message


            });


        }



        [HttpPost]
        public ActionResult addStudentt(addStudntDTO dto)
        {


            bool good = Checking.adminAdd(dto.Id, dto.Name, dto.Schedule);
            if (!good) return Conflict($"A student with ID '{dto.Id}' already exists.");
            return CreatedAtAction( nameof(getSchedd), new { id = dto.Id }, dto);


        }



        [HttpPut("{id}/schedule")]
        public ActionResult updateSchde(string id, updateScheduleDTO dto)
        {


            if (!Checking.checkId(id))
                return NotFound($"Student '{id}' not found.");


            bool good = Checking.adminUpdate(id, dto.Schedule);
            return good ? NoContent() : BadRequest("Schedule update failed.");


        }

        

        [HttpDelete("{id}")]
        public ActionResult deleteStudentt(string id)
        {


            if (!Checking.checkId(id))
                return NotFound($"Student '{id}' not found.");


            bool good = Checking.adminDelete(id);
            return good ? NoContent() : BadRequest("Delete failed.");


        }


    }

    

    public class studentDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class schedDTO
    {
        public string Id { get; set; }
        public IEnumerable<string> Schedule { get; set; }
    }

    public class recrdDTO
    {
        public string Id { get; set; }
        public int Lates { get; set; }
        public int Absents { get; set; }
    }

    public class inOutDTO
    {
        public string Id { get; set; }
        public string Message { get; set; }
    }

    public class addStudntDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Schedule { get; set; }
    }

    public class updateScheduleDTO
    {
        public List<string> Schedule { get; set; }
    }
}