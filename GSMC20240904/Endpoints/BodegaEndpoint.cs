using Microsoft.AspNetCore.Authorization;

namespace GSMC20240904.Endpoints
{
    public static class BodegaEndpoint
    {

        // Lista estática en memoria para almacenar las bodegas
        static List<object> bodegas = new List<object>();

        public static void AddBodegaEndpoints(this WebApplication app)
        {
            app.MapGet("/bodega", () =>
            {
                return bodegas;
            }).RequireAuthorization();

            app.MapPost("/bodega", (int id, string nombre, string ubicacion) =>
            {
                var bodega = new
                {
                    id,  // Usando el id proporcionado
                    nombre,
                    ubicacion
                };
                bodegas.Add(bodega);
                return bodegas;
            }).RequireAuthorization();

            app.MapGet("/bodega/{id}", (int id) =>
            {
                var bodega = bodegas.FirstOrDefault(m => m.GetType().GetProperty("id")?.GetValue(m).Equals(id) == true);
                if (bodega == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(bodega);
            }).RequireAuthorization();
            app.MapPut("/bodega/{id}", (int id, string nombre, string ubicacion) =>
            {
                var bodega = bodegas.FirstOrDefault(m => m.GetType().GetProperty("id")?.GetValue(m).Equals(id) == true);
                if (bodega == null)
                {
                    return Results.NotFound();
                }

                // Crear una nueva bodega con los datos actualizados
                var updatedBodega = new
                {
                    id,
                    nombre,
                    ubicacion
                };

                // Remover la bodega existente
                bodegas.Remove(bodega);
                // Añadir la bodega actualizada
                bodegas.Add(updatedBodega);

                return Results.Ok(updatedBodega);
            }).RequireAuthorization();


            app.MapDelete("/bodega/{id}", (int id) =>
            {
                var bodega = bodegas.FirstOrDefault(m => m.GetType().GetProperty("id")?.GetValue(m).Equals(id) == true);
                if (bodega == null)
                {
                    return Results.NotFound();
                }

                bodegas.Remove(bodega);
                return Results.Ok(bodega);
            }).RequireAuthorization();
        }
    }
}
