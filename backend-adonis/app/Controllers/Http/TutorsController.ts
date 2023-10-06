import { HttpContextContract } from '@ioc:Adonis/Core/HttpContext'
import Tutor from 'App/Models/Tutor'

export default class TutorsController {

    public async store({ request, response }: HttpContextContract) {

        const body = request.body()

        const tutor = await Tutor.create(body)

        response.status(201)
        return {
            data: tutor,
        }
    }

    public async index() {
        //const tutors = await Tutor.all()
        const tutors = await Tutor.query().preload('enderecos')

        return {
            data: tutors,
        }
    }

    public async show({ params }: HttpContextContract) {
        const tutor = await Tutor.findOrFail(params.id)

        await tutor.load('enderecos')

        return {
            data: tutor,
        }
    }

    public async destroy({ params }: HttpContextContract) {
        const tutor = await Tutor.findOrFail(params.id)

        await tutor.delete()
        return {
            data: tutor,
        }
    }

    public async update({ params, request }: HttpContextContract) {

        const body = request.body()
        const tutor = await Tutor.findOrFail(params.id)

        tutor.nome = body.nome
        tutor.nome_normalizado = body.nomeNormalizado
        tutor.cpf = body.cpf
        tutor.telefone = body.telefone
        tutor.email = body.email

        await tutor.save()
        return {
            data: tutor,
        }
    }
}