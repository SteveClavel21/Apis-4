using Microsoft.AspNetCore.Authorization;

namespace GSMC20240904.Endpoints
{
    public static class BodegaEndpoint
    {

        // Lista estática en memoria para almacenar las bodegas
        static List<object> bodegas = new List<object>();
    
        public static void AddBodegaEndpoints(this WebApplication app)
        {
            // Endpoint privado para crear una nueva bodega
            app.MapPost("/bodega/crear", [Authorize] (Guid id, string nombre, string ubicacion) =>
            {
                // Agrega la nueva bodega con el ID proporcionado por el usuario
                var nuevaBodega = new { Id = id, Nombre = nombre, Ubicacion = ubicacion };
                bodegas.Add(nuevaBodega);
                return Results.Ok(nuevaBodega);
            }).RequireAuthorization();
            // Endpoint privado para modificar una bodega existente
            app.MapPut("/bodega/modificar", [Authorize] (Guid id, string nombre, string ubicacion) =>
            {
                var bodega = bodegas.FirstOrDefault(b => ((dynamic)b).Id == id);
                if (bodega == null)
                {
                    return Results.NotFound("Bodega no encontrada.");
                }

                // Modificar los detalles de la bodega con el ID proporcionado por el usuario
                ((dynamic)bodega).Nombre = nombre;
                ((dynamic)bodega).Ubicacion = ubicacion;
                return Results.Ok("Bodega modificada exitosamente.");
            }).RequireAuthorization();

            // Endpoint privado para obtener una bodega por su ID
            app.MapGet("/bodega/{id}", [Authorize] (Guid id) =>
            {
                var bodega = bodegas.FirstOrDefault(b => ((dynamic)b).Id == id);
                if (bodega == null)
                {
                    return Results.NotFound("Bodega no encontrada.");
                }
                return Results.Ok(bodega);
            }).RequireAuthorization();
        }
    }
}
