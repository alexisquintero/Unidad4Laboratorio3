using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Ejercicio1
{
    class Program
    {
        static void Main(string[] args)
        {
            //DataTables

 /*           DataTable dtAlumnos = new DataTable();
            DataRow rwAlumno = dtAlumnos.NewRow();
            DataColumn colNombre = new DataColumn("Nombre");
            DataColumn colApellido = new DataColumn("Apellido");

            dtAlumnos.Columns.Add(colApellido);
            dtAlumnos.Columns.Add(colNombre);

            rwAlumno[colApellido] = "Perez";
            rwAlumno[colNombre] = "Juan";

            dtAlumnos.Rows.Add(rwAlumno);

            DataRow rwAlumno2 = dtAlumnos.NewRow();
            rwAlumno2["Apellido"] = "Perez";
            rwAlumno2["Nombre"] = "Marcelo";
            dtAlumnos.Rows.Add(rwAlumno2);

            Console.WriteLine("Listado de Alumnos: ");
            foreach (DataRow row in dtAlumnos.Rows)
            {
                Console.WriteLine(row[colApellido].ToString() + ", " + row[colNombre].ToString());
            }
            Console.ReadLine();
 */
//----------------------------------------------------------------------

            //DataSets no tipados
            DataTable dtAlumnos = new DataTable();
            DataColumn colIDAlumno = new DataColumn("IDAlumno", typeof(int));
            colIDAlumno.ReadOnly = true;
            colIDAlumno.AutoIncrement = true;
            colIDAlumno.AutoIncrementSeed = 0;
            colIDAlumno.AutoIncrementStep = 1;
            DataColumn colNombre = new DataColumn("Nombre", typeof(string));
            DataColumn colApellido = new DataColumn("Apellido", typeof(string));
            dtAlumnos.Columns.Add(colApellido);
            dtAlumnos.Columns.Add(colNombre);
            dtAlumnos.Columns.Add(colIDAlumno);
            dtAlumnos.PrimaryKey = new DataColumn[] { colIDAlumno };
            DataRow rwAlumno = dtAlumnos.NewRow();
            rwAlumno[colApellido] = "Perez";
            rwAlumno[colNombre] = "Juan";

            dtAlumnos.Rows.Add(rwAlumno);

            DataRow rwAlumno2 = dtAlumnos.NewRow();
            rwAlumno2["Apellido"] = "Perez";
            rwAlumno2["Nombre"] = "Marcelo";
            dtAlumnos.Rows.Add(rwAlumno2);

            DataTable dtCursos = new DataTable("Cursos");
            DataColumn colIdCurso = new DataColumn("IDCurso", typeof(int));
            colIdCurso.ReadOnly = true;
            colIdCurso.AutoIncrement = true;
            colIdCurso.AutoIncrementSeed = 0;
            colIdCurso.AutoIncrementStep = 1;
            DataColumn colCurso = new DataColumn("Curso", typeof(String));
            dtCursos.Columns.Add(colIdCurso);
            dtCursos.Columns.Add(colCurso);
            dtCursos.PrimaryKey = new DataColumn[] { colIdCurso };

            DataRow rwCurso = dtCursos.NewRow();
            rwCurso[colCurso] = "Informatica";
            dtCursos.Rows.Add(rwCurso);

            DataSet dsUNiversidad = new DataSet();
            dsUNiversidad.Tables.Add(dtAlumnos);
            dsUNiversidad.Tables.Add(dtCursos);

            DataTable dtAlumnos_Cursos = new DataTable("Alumnos_Cursos");
            DataColumn col_ac_IDAlumno = new DataColumn("IDAlumno", typeof(int));
            DataColumn col_ac_IDCurso = new DataColumn("IDCurso", typeof(int));
            dtAlumnos_Cursos.Columns.Add(col_ac_IDAlumno);
            dtAlumnos_Cursos.Columns.Add(col_ac_IDCurso);

            dsUNiversidad.Tables.Add(dtAlumnos_Cursos);
            DataRelation relAlumno_ac = new DataRelation("Alumno_Cursos", colIDAlumno, col_ac_IDAlumno);
            DataRelation relCurso_ac = new DataRelation("Curso_alumnos", colIdCurso, col_ac_IDCurso);
            dsUNiversidad.Relations.Add(relAlumno_ac);           
            dsUNiversidad.Relations.Add(relCurso_ac);

            dsUNiversidad.EnforceConstraints = false; //Tira error sin esta línea

            DataRow rwAlumnosCursos = dtAlumnos_Cursos.NewRow();
            rwAlumnosCursos[col_ac_IDAlumno] = 0;
            rwAlumnosCursos[col_ac_IDCurso] = 0; //Informatica es 0, no 1
            dtAlumnos_Cursos.Rows.Add(rwAlumnosCursos);

            rwAlumnosCursos = dtAlumnos_Cursos.NewRow();
            rwAlumnosCursos[col_ac_IDAlumno] = 1;
            rwAlumnosCursos[col_ac_IDCurso] = 0; //Informatica es 0, no 1
            dtAlumnos_Cursos.Rows.Add(rwAlumnosCursos);

            Console.Write("Por favor ingrese el nombre del curso: ");
            string materia = Console.ReadLine();
            Console.WriteLine("Listado de alumnos del curso " + materia);
            DataRow[] row_cursoInf = dtCursos.Select("Curso = '" + materia + "'");
            foreach (DataRow rowCu in row_cursoInf)
            {
                DataRow[] row_AlumnosInf = rowCu.GetChildRows(relCurso_ac);
                foreach (DataRow rowAl in row_AlumnosInf)
                {
                    Console.WriteLine(rowAl.GetParentRow(relAlumno_ac)[colApellido].ToString() + ", " +
                                        rowAl.GetParentRow(relAlumno_ac)[colNombre].ToString());
                }
            }
            Console.ReadLine();
//----------------------------------------------------------------------------
            //DataSets Tipados

        }
    }
}
