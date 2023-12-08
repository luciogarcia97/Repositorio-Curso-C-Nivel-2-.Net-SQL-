

select a.Id Id, a.Codigo Codigo, a.Nombre Nombre, a.Descripcion Descripcion, c.Descripcion Categoria, m.Descripcion Marca, a.Precio Precio, c.id idCategoria, m.Id idMarcas, a.ImagenUrl from ARTICULOS a left join CATEGORIAS c on c.Id = a.IdCategoria left join MARCAS m on m.Id = a.IdMarca

select * from ARTICULOS
select * from CATEGORIAS
select * from MARCAS