import BaseSchema from '@ioc:Adonis/Lucid/Schema'

export default class Pets extends BaseSchema {
  protected tableName = 'pets'

  public async up () {
    this.schema.createTable(this.tableName, (table) => {
      table.increments('id').primary()
      table.string('nome')
      table.integer('idade_mes')
      table.string('raca')
      table.string('sexo')
      table.decimal('peso', 10, 2)
      table.string('especie')

      table.integer('tutor_id').unsigned().references('id').inTable('tutors').onDelete('CASCADE')
      
      table.timestamp('created_at', { useTz: true })
      table.timestamp('updated_at', { useTz: true })
    })
  }

  public async down () {
    this.schema.dropTable(this.tableName)
  }
}