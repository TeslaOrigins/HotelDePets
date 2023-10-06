import { DateTime } from 'luxon';
import { BaseModel, BelongsTo, belongsTo, column } from '@ioc:Adonis/Lucid/Orm';
import Tutor from './Tutor';

export default class Pet extends BaseModel {
  @column({ isPrimary: true })
  public id: number;

  @column()
  public nome: string;

  @column()
  public idade_mes: number;

  @column()
  public raca: string;

  @column()
  public sexo: string;

  @column()
  public peso: number;

  @column()
  public especie: string;

  @column()
  public tutorId: number;

  @belongsTo(() => Tutor, {
    foreignKey: 'tutorId', 
  })
  public tutor: BelongsTo<typeof Tutor>;

  @column.dateTime({ autoCreate: true })
  public createdAt: DateTime;

  @column.dateTime({ autoCreate: true, autoUpdate: true })
  public updatedAt: DateTime;
}