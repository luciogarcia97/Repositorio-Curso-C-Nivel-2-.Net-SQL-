--Query de consultas

/*
1. Traer todos los discos con su respectivo estilo. La consulta debe mostrar: Nombre de disco, Fecha de lanzamiento, Estilo (no el id, sino la descripción).

2. Insertar al menos dos estilos nuevos y un tipo de edición de disco. 

3. Insertar al menos dos discos nuevos cargando correctamente su información.

4. Actualizar al menos un disco modificando la cantidad de canciones y la fecha de lanzamiento. No te olvides del Where.

5. Borrar un disco a elección.

6. Traer todos los estilos que estén asociados a algún disco.

7. Traer todos los discos con el siguiente formato: Nombre, Estilo, Edición (todo texto).

8. Traer todos los discos que contengan en su nombre la sílaba "ho".
*/

/*
--1
select d.Titulo, d.FechaLanzamiento, e.Descripcion from DISCOS d
left join ESTILOS e on d.IdEstilo = e.Id;

--2
--insert into ESTILOS values ('Blues');
--select * from ESTILOS;

select * from TIPOSEDICION;
--insert into TIPOSEDICION values ('Spotify');

--3
select * from DISCOS;
--select * from ESTILOS
--select * from TIPOSEDICION;
--insert into DISCOS values ('Ahi vamos','2006-04-04 00:00:00',13,'https://images.genius.com/5a073c940667ebe29d529471e5708bbb.1000x800x1.jpg',3,2);
--insert into DISCOS values ('Mucho','2008-05-08 12:00:00',10,'https://www.cmtv.com.ar/tapas-cd/babasonicosmucho.webp',7,2);
--insert into ESTILOS values ('Rock Alternativo');

--4
select * from discos;
--update discos set FechaLanzamiento = '1986-06-22', CantidadCanciones = 11 where Id = 1;

--5
--insert into DISCOS values ('Clics modernos','1983-11-05 11:12:00',9,'https://i.scdn.co/image/ab67616d0000b273b14842a87b833bc0a9339f60',8,1);
--insert into ESTILOS values ('New Wave');
--select * from estilos;
--select * from TIPOSEDICION;
select * from discos;
--delete from DISCOS where id = 2;

--6
--select * from ESTILOS e inner join DISCOS d on d.IdEstilo = e.Id; 
select * from DISCOS d inner join ESTILOS e on d.IdEstilo = e.Id; 
--select * from DISCOS d left join ESTILOS e on d.IdEstilo = e.Id; 
--select * from ESTILOS e left join DISCOS d on d.IdEstilo = e.Id; 
--select * from ESTILOS;

--7
select d.Titulo as Nombre, e.Descripcion as Estilo, tp.Descripcion Edicion from DISCOS d left join ESTILOS e on d.IdEstilo = e.Id left join TIPOSEDICION tp on d.IdTipoEdicion = tp.Id;

--8
select * from discos where Titulo like '%ho%';
*/