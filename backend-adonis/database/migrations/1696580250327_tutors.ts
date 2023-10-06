import BaseSchema from '@ioc:Adonis/Lucid/Schema'

export default class Tutors extends BaseSchema {
  protected tableName = 'tutors'

  public async up () {
    this.schema.createTable(this.tableName, (table) => {
      table.increments('id')

      table.string('nome')
      table.string('nome_normalizado')
      table.string('cpf')
      table.string('telefone')
      table.string('email')

      //table.integer('endereco_id').unsigned().references('id').inTable('enderecos').onDelete('CASCADE')

      table.timestamp('created_at', { useTz: true })
      table.timestamp('updated_at', { useTz: true })
    })
  }

  public async down () {
    this.schema.dropTable(this.tableName)
  }
}