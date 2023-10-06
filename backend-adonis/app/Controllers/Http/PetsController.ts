import { HttpContextContract } from '@ioc:Adonis/Core/HttpContext'
import Pet from 'App/Models/Pet'

export default class PetsController {

  public async store({ request, response }: HttpContextContract) {
    const body = request.body()

    const pet = await Pet.create(body)

    response.status(201)
    return {
      data: pet,
    }
  }

  public async index() {
    const pets = await Pet.query().preload('tutor')

    return {
      data: pets,
    }
  }

  public async show({ params }: HttpContextContract) {
    const pet = await Pet.findOrFail(params.id)

    await pet.load('tutor')

    return {
      data: pet,
    }
  }

  public async destroy({ params }: HttpContextContract) {
    const pet = await Pet.findOrFail(params.id)

    await pet.delete()
    return {
      data: pet,
    }
  }

  public async update({ params, request }: HttpContextContract) {
    const body = request.body()
    const pet = await Pet.findOrFail(params.id)

    pet.nome = body.nome
    pet.idade_mes = body.idadeMes
    pet.raca = body.raca
    pet.sexo = body.sexo
    pet.peso = body.peso
    pet.especie = body.especie
    pet.tutorId = body.tutorId // Certifique-se de que o campo correto é 'tutorId' no seu modelo Pet

    await pet.save()
    return {
      data: pet,
    }
  }
}