using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using FairyBE.Models.EventModels;

namespace FairyBE.Controllers.EventControllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin, Manager")]
    public class EventsController : Controller
    {
        private readonly NpgsqlConnection _connection;
        public IConfiguration Configuration { get; }

        public EventsController(IConfiguration config)
        {
            Configuration = config;
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            _connection = new NpgsqlConnection(connectionString);
        }

        #region Métodos CRUD Generales

        private async Task<IActionResult> ListAllRecords<T>(string tableName)
        {
            try
            {
                string selectQuery = $"SELECT * FROM {tableName}";
                _connection.Open();
                var result = await _connection.QueryAsync<T>(selectQuery);
                _connection.Close();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _connection.Close();
                return BadRequest(ex.Message);
            }
        }

        private async Task<IActionResult> GetRecordById<T>(string tableName, long id)
        {
            try
            {
                string selectQuery = $"SELECT * FROM {tableName} WHERE id = @Id";
                _connection.Open();
                var result = await _connection.QueryFirstOrDefaultAsync<T>(selectQuery, new { Id = id });
                _connection.Close();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _connection.Close();
                return BadRequest(ex.Message);
            }
        }

        private async Task<IActionResult> CreateRecord<T>(string tableName, T model)
        {
            try
            {
                string insertQuery = $"INSERT INTO {tableName} VALUES (@{string.Join(", @", typeof(T).GetProperties().Select(p => p.Name))}) RETURNING id";
                _connection.Open();
                var result = await _connection.ExecuteAsync(insertQuery, model);
                await LogAudit(tableName, result, "INSERT", model);
                _connection.Close();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _connection.Close();
                return BadRequest(ex.Message);
            }
        }

        private async Task<IActionResult> UpdateRecord<T>(string tableName, long id, T model)
        {
            try
            {
                var updateQuery = $"UPDATE {tableName} SET {string.Join(", ", typeof(T).GetProperties().Where(p => p.Name != "Id").Select(p => $"{p.Name} = @{p.Name}"))} WHERE id = @Id";
                _connection.Open();
                var result = await _connection.ExecuteAsync(updateQuery, new { Id = id, model });
                await LogAudit(tableName, id, "UPDATE", model);
                _connection.Close();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _connection.Close();
                return BadRequest(ex.Message);
            }
        }

        private async Task<IActionResult> DeleteRecord<T>(string tableName, long id)
        {
            try
            {
                string deleteQuery = $"DELETE FROM {tableName} WHERE id = @Id";
                _connection.Open();
                var result = await _connection.ExecuteAsync(deleteQuery, new { Id = id });
                await LogAudit(tableName, id, "DELETE", default(T));
                _connection.Close();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _connection.Close();
                return BadRequest(ex.Message);
            }
        }

        private async Task LogAudit<T>(string tableName, long recordId, string action, T model)
        {
            var userId = User.Identity.Name ?? "0"; // Captura el usuario autenticado

            var auditData = new
            {
                entity_name = tableName,
                record_id = recordId,
                action = action,
                user_id = userId,
                timestamp = DateTime.UtcNow,
                changes = model != null ? JsonConvert.SerializeObject(model) : null
            };

            string auditQuery = "INSERT INTO audit_log (entity_name, record_id, action, user_id, timestamp, changes) VALUES (@entity_name, @record_id, @action, @user_id, @timestamp, @changes)";
            await _connection.ExecuteAsync(auditQuery, auditData);
        }
        #endregion

        #region Endpoints CRUD

        [HttpPost("Register/{entity}")]
        public Task<IActionResult> RegisterEntity<T>(string entity, [FromBody] T model) => CreateRecord(entity, model);

        [HttpPut("Update/{entity}/{id}")]
        public Task<IActionResult> UpdateEntity<T>(string entity, long id, [FromBody] T model) => UpdateRecord(entity, id, model);

        [HttpDelete("Delete/{entity}/{id}")]
        public Task<IActionResult> DeleteEntity<T>(string entity, long id) => DeleteRecord<T>(entity, id);

        [HttpGet("GetById/{entity}/{id}")]
        public Task<IActionResult> GetById<T>(string entity, long id) => GetRecordById<T>(entity, id);

        [HttpGet("ListAll/{entity}")]
        public Task<IActionResult> ListAll<T>(string entity) => ListAllRecords<T>(entity);

        #endregion
    }
}
