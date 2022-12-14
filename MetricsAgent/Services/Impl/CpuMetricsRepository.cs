using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.Services.Impl
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public CpuMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public void Create(CpuMetric item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("INSERT INTO cpumetrics(value, time) VALUEs(@value, @time)", new
            {
                value = item.Value,
                time = item.Time
            });

            { 
                //connection.Open();
                ////Создаем команду
                //using var cmd = new SQLiteCommand(connection);
                ////Прописываем в команду SQL-запрос на вставку данных
                //cmd.CommandText = "INSERT INTO cpumetrics(value, time) VALUEs(@value, @time)";
                ////Добавляем параметры в запрос из нашего объекта
                //cmd.Parameters.AddWithValue("@value", item.Value);
                ////В таблице будем хранить время в секуднах
                //cmd.Parameters.AddWithValue("@time", item.Time);
                ////Подготовка команды к выполнению
                //cmd.Prepare();
                ////Выполнение команды
                //cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("DELETE FROM cpumetrics WHERE id=@id", new
            {
                id = id
            });

            {
                //connection.Open();
                //using var cmd = new SQLiteCommand(connection);
                ////Прописываем в команду SQL-запрос на удаление данных
                //cmd.CommandText = "DELETE FROM cpumetrics WHERE id=@id";
                //cmd.Parameters.AddWithValue("@id", id);
                //cmd.Prepare();
                //cmd.ExecuteNonQuery();
            }
        }

        public IList<CpuMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.Query<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics").ToList();

            {
                //connection.Open();
                //using var cmd = new SQLiteCommand(connection);
                ////Прописываем в команду SQL-запрос на получение данных из таблицы
                //cmd.CommandText = "SELECT * FROM cpumetrics";
                //var returnList = new List<CpuMetric>();
                //using (SQLiteDataReader reader = cmd.ExecuteReader())
                //{
                //    //Пока есть что читать - читаем
                //    while (reader.Read())
                //    {
                //        returnList.Add(new CpuMetric
                //        {
                //            Id = reader.GetInt32(0),
                //            Value = reader.GetInt32(1),
                //            Time = reader.GetInt32(2),
                //        });
                //    }
                //}
                //return returnList;
            }
        }

        public CpuMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.QuerySingle<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics WHERE id = @id", new {id = id});

            {
                //connection.Open();
                //using var cmd = new SQLiteCommand(connection);
                //cmd.CommandText = "SELECT * FROM cpumetrics WHERE id=@id";
                //using (SQLiteDataReader reader = cmd.ExecuteReader())
                //{
                //    //Если удалось что-то прочитать
                //    if (reader.Read())
                //    {
                //        //возвращаем прочитанное
                //        return new CpuMetric
                //        {
                //            Id = reader.GetInt32(0),
                //            Value = reader.GetInt32(1),
                //            Time = reader.GetInt32(2)
                //        };
                //    }
                //    else
                //    {
                //        //Не нашлась запись по идентификатору, не делаем ничего
                //        return null;
                //    }
                //}
            }
        }
        
        public IList<CpuMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.Query<CpuMetric>("SELECT * FROM cpumetrics WHERE time >= @timeFrom and time <= @timeTo",
                new {
                    timeFrom = timeFrom.TotalSeconds,
                    timeTo = timeTo.TotalSeconds
                }).ToList();

            {
                //connection.Open();
                //using var cmd = new SQLiteCommand(connection);
                ////Прописываем в команду SQL-запрос на получение всех данных за период из таблицы
                //cmd.CommandText = "SELECT * FROM cpumetrics where time >= @timeFrom and time <= @timeTo";
                //cmd.Parameters.AddWithValue("@timeFrom", timeFrom.TotalSeconds);
                //cmd.Parameters.AddWithValue("@timeTo", timeTo.TotalSeconds);
                //var returnList = new List<CpuMetric>();
                //using (SQLiteDataReader reader = cmd.ExecuteReader())
                //{
                //    //Пока есть что читать — читаем
                //    while (reader.Read())
                //    {
                //        //Добавляем объект в список возврата
                //        returnList.Add(new CpuMetric
                //        {
                //            Id = reader.GetInt32(0),
                //            Value = reader.GetInt32(1),
                //            Time = reader.GetInt32(2)
                //        });
                //    }
                //}
                //return returnList;
            }
        }

        public void Update(CpuMetric item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("UPDATE cpumetrics SET value = @value, time = @time WHERE id = @id; ",
                new {
                    value = item.Value,
                    time = item.Time,
                    id = item.Id
                });

            {
                //using var cmd = new SQLiteCommand(connection);
                ////Прописываем в команду SQL-запрос на обновление данных
                //cmd.CommandText = "UPDATE cpumetrics SET value = @value, time = @time WHERE id = @id; ";
                //cmd.Parameters.AddWithValue("@id", item.Id);
                //cmd.Parameters.AddWithValue("@value", item.Value);
                //cmd.Parameters.AddWithValue("@time", item.Time);
                //cmd.Prepare();
                //cmd.ExecuteNonQuery();
            }
        }
    }
}
