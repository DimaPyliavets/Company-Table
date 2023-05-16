using CompTable.Data;
using CompTable.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompTable.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableControllerApi : Controller
    {
        private readonly TableModelContext _tableModelContextDb;

        public TableControllerApi(TableModelContext tableModelContextDb)
        {
            _tableModelContextDb = tableModelContextDb;
        }

        [HttpGet("GetAllItems")]
        public async Task<ActionResult<IEnumerable<TableModel>>> GetTable()
        {
            if (_tableModelContextDb.TableModels == null)
            {
                return NotFound();
            }
            return await _tableModelContextDb.TableModels.ToListAsync();
        }

        [HttpGet("GetItem/{id}")]
        public async Task<ActionResult<TableModel>> GetTable(int id)
        {
            if (_tableModelContextDb.TableModels == null)
            {
                return NotFound();
            }
            var table = await _tableModelContextDb.TableModels.FindAsync(id);
            if (table == null)
            {
                return NotFound();
            }
            return table;
        }

        [HttpPost("AddNewItem")]
        public async Task<ActionResult<TableModel>> PostTableElement(TableModel table)
        {
            _tableModelContextDb.TableModels.Add(table);
            await _tableModelContextDb.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTable), new { id = table.ID }, table);
        }

        [HttpPut("UpdateItem/{id}")]
        public async Task<IActionResult> PutTableElement(int id, TableModel table)
        {
            if (id != table.ID)
            {
                return BadRequest();
            }
            _tableModelContextDb.Entry(table).State = EntityState.Modified;

            try
            {
                await _tableModelContextDb.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TableElementAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        private bool TableElementAvailable(int id)
        {
            return _tableModelContextDb.TableModels.Any(x => x.ID == id);
        }

        [HttpDelete("DeleteItem/{id}")]
        public async Task<IActionResult> DeleteTableElement(int id)
        {
            if (_tableModelContextDb.TableModels == null)
            {
                return NotFound();
            }
            var table = await _tableModelContextDb.TableModels.FindAsync(id);
            if (table == null)
            {
                return NotFound();
            }

            _tableModelContextDb.TableModels.Remove(table);

            await _tableModelContextDb.SaveChangesAsync();

            return Ok();
        }
    }
}
