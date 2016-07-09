using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WebEAD.DAO
{
    public class Auxiliares
    {
        ConexaoBD cxbd = new ConexaoBD();

        private MySqlDataAdapter _mDA;
        private MySqlCommand _mCMD;
        private DataTable _dt;
        private string _sql, rtrInsert, rtrUpdate, rtrDelete;

        public DataTable Select(string campos, string resto)
        {
            try
            {
                _sql = "SELECT " + campos + " FROM " + resto + "";
                _mDA = new MySqlDataAdapter(_sql, cxbd.AbreConexao());
                _dt = new DataTable();
                _mDA.Fill(_dt);
                return _dt;
            }
            catch (MySqlException mysql)
            {
                return _dt;
            }
            catch (Exception ex)
            {
                return _dt;
            }
            cxbd.FechaConexao();
        }

        public bool SelectBool(string campos, string resto)
        {
            try
            {
                _sql = "SELECT " + campos + " FROM " + resto + "";
                _mDA = new MySqlDataAdapter(_sql, cxbd.AbreConexao());
                _dt = new DataTable();
                _mDA.Fill(_dt);
                if (_dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException mysql)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            cxbd.FechaConexao();
        }

        public string Insert(string tabela, string resto)
        {
            try
            {
                _sql = "INSERT INTO " + tabela + " " + resto;
                _mCMD = new MySqlCommand(_sql, cxbd.AbreConexao());
                int _vCMD = _mCMD.ExecuteNonQuery();
                if (_vCMD >= 1)
                {
                    rtrInsert = "true";
                    return rtrInsert;
                }
                else
                {
                    rtrInsert = "false";
                    return rtrInsert;
                }
            }
            catch (MySqlException mysql)
            {
                rtrInsert = mysql.Number.ToString();
                return rtrInsert;
            }
            catch (Exception ex)
            {
                rtrInsert = ex.HResult.ToString();
                return rtrInsert;
            }
            cxbd.FechaConexao();
        }

        public string Update(string tabela, string colunasvalores, string condicao)
        {
            try
            {
                _sql = "UPDATE " + tabela + " SET " + colunasvalores + " WHERE " + condicao + "";
                _mCMD = new MySqlCommand(_sql, cxbd.AbreConexao());
                int _vCMD = _mCMD.ExecuteNonQuery();
                if (_vCMD == 1)
                {
                    rtrUpdate = "true";
                    return rtrUpdate;
                }
                else
                {
                    rtrUpdate = "false";
                    return rtrUpdate;
                }
            }
            catch (MySqlException mysql)
            {
                rtrUpdate = mysql.Number.ToString();
                return rtrUpdate;
            }
            catch (Exception ex)
            {
                rtrUpdate = ex.HResult.ToString();
                return rtrUpdate;
            }
            cxbd.FechaConexao();
        }

        public string Delete(string tabela, string condicao)
        {
            try
            {
                _sql = "DELETE FROM " + tabela + " WHERE " + condicao + "";
                _mCMD = new MySqlCommand(_sql, cxbd.AbreConexao());
                int _vCMD = _mCMD.ExecuteNonQuery();
                if (_vCMD >= 1)
                {
                    rtrDelete = "true";
                    return rtrDelete;
                }
                else
                {
                    rtrDelete = "false";
                    return rtrDelete;
                }
            }
            catch (MySqlException mysql)
            {
                rtrDelete = mysql.Number.ToString();
                return rtrDelete;
            }
            catch (Exception ex)
            {
                rtrDelete = ex.HResult.ToString();
                return rtrDelete;
            }
            cxbd.FechaConexao();
        }
    }
}