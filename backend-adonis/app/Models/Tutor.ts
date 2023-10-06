import { DateTime } from 'luxon';
import { BaseModel, column, hasMany, HasMany, BelongsTo, belongsTo } from '@ioc:Adonis/Lucid/Orm';
import Endereco from 'App/Models/Endereco';
import Pet from 'App/Models/Pet';

export default class Tutor extends BaseModel {
  @column({ isPrimary: true })
  public id: number;

  @column()
  public nome: string;

  @column()
  public nome_normalizado: string;

  @column()
  public cpf: string;

  @column()
  public telefone: string;

  @column()
  public email: string;

  @hasMany(() => Endereco)
  public enderecos: HasMany<typeof Endereco>;

  @belongsTo(() => Pet, {
    foreignKey: 'tutorId',
  })
  public pets: BelongsTo<typeof Pet>;

  @column.dateTime({ autoCreate: true })
  public createdAt: DateTime;

  @column.dateTime({ autoCreate: true, autoUpdate: true })
  public updatedAt: DateTime;
}