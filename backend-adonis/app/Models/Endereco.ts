import { DateTime } from 'luxon';
import { BaseModel, column } from '@ioc:Adonis/Lucid/Orm';

export default class Endereco extends BaseModel {
  @column({ isPrimary: true })
  public id: number;

  @column()
  public logradouro: string;

  @column()
  public numero: string;

  @column()
  public cidade: string;

  @column()
  public estado: string;

  @column()
  public tutorId: number;

  @column.dateTime({ autoCreate: true })
  public createdAt: DateTime;

  @column.dateTime({ autoCreate: true, autoUpdate: true })
  public updatedAt: DateTime;
}